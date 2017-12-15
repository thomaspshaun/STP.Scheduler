using Hangfire;
using Hangfire.Common;
using Hangfire.States;
using Hangfire.Storage;
using NLog;
using STP.Scheduler.Infrastructure.Logging;
using System;

namespace STP.Scheduler.Infrastructure
{
  public sealed class AutomaticRetryAttribute : JobFilterAttribute, IElectStateFilter, IApplyStateFilter
  {
    /// <summary>
    /// Represents the default number of retry attempts. This field is read-only.
    /// </summary>
    /// <remarks>
    /// The value of this field is <c>10</c>.
    /// </remarks>
    public static readonly int DefaultRetryAttempts = 1;
    private static readonly ILogger Logger = (ILogger)LogManager.GetCurrentClassLogger();

    private readonly object _lockObject = new object();
    private int _attempts;
    private AttemptsExceededAction _onAttemptsExceeded;
    private bool _logEvents;

    /// <summary>
    /// Initializes a new instance of the <see cref="AutomaticRetryAttribute"/>
    /// class with <see cref="DefaultRetryAttempts"/> number.
    /// </summary>
    public AutomaticRetryAttribute()
    {
      Attempts = DefaultRetryAttempts;
      LogEvents = true;
      OnAttemptsExceeded = AttemptsExceededAction.Fail;
    }

    /// <summary>
    /// Gets or sets the maximum number of automatic retry attempts.
    /// </summary>
    /// <value>Any non-negative number.</value>
    /// <exception cref="ArgumentOutOfRangeException">The value in a set operation is less than zero.</exception>
    public int Attempts
    {
      get { lock (_lockObject) { return _attempts; } }
      set
      {
        if (value < 0)
        {
          throw new ArgumentOutOfRangeException("value", "Attempts value must be equal or greater than zero.");
        }

        lock (_lockObject)
        {
          _attempts = value;
        }
      }
    }

    /// <summary>
    /// Gets or sets a candidate state for a background job that 
    /// will be chosen when number of retry attempts exceeded.
    /// </summary>
    public AttemptsExceededAction OnAttemptsExceeded
    {
      get { lock (_lockObject) { return _onAttemptsExceeded; } }
      set { lock (_lockObject) { _onAttemptsExceeded = value; } }
    }

    /// <summary>
    /// Gets or sets whether to produce log messages on retry attempts.
    /// </summary>
    public bool LogEvents
    {
      get { lock (_lockObject) { return _logEvents; } }
      set { lock (_lockObject) { _logEvents = value; } }
    }

    /// <inheritdoc />
    public void OnStateElection(ElectStateContext context)
    {

      var failedState = context.CandidateState as FailedState;
      if (failedState == null)
      {
        // This filter accepts only failed job state.
        return;
      }

      var retryAttempt = context.GetJobParameter<int>("RetryCount") + 1;

      if (retryAttempt <= Attempts)
      {
        ScheduleAgainLater(context, retryAttempt, failedState);
      }
      else if (retryAttempt > Attempts && OnAttemptsExceeded == AttemptsExceededAction.Delete)
      {
        TransitionToDeleted(context, failedState);
      }
      else
      {
        if (LogEvents)
        {
          Logger.ErrorException(
              $"Failed to process the job '{context.BackgroundJob.Id}': an exception occurred.",
              failedState.Exception);
        }
      }
    }

    /// <inheritdoc />
    public void OnStateApplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
    {
      if (context.NewState is ScheduledState &&
          context.NewState.Reason != null &&
          context.NewState.Reason.StartsWith("Retry attempt"))
      {
        transaction.AddToSet("retries", context.BackgroundJob.Id);
      }
    }

    /// <inheritdoc />
    public void OnStateUnapplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
    {
      if (context.OldStateName == ScheduledState.StateName)
      {
        transaction.RemoveFromSet("retries", context.BackgroundJob.Id);
      }
    }

    /// <summary>
    /// Schedules the job to run again later. See <see cref="SecondsToDelay"/>.
    /// </summary>
    /// <param name="context">The state context.</param>
    /// <param name="retryAttempt">The count of retry attempts made so far.</param>
    /// <param name="failedState">Object which contains details about the current failed state.</param>
    private void ScheduleAgainLater(ElectStateContext context, int retryAttempt, FailedState failedState)
    {
      context.SetJobParameter("RetryCount", retryAttempt);

      var delay = TimeSpan.FromSeconds(SecondsToDelay(retryAttempt));

      const int maxMessageLength = 50;
      var exceptionMessage = failedState.Exception.Message;

      // If attempt number is less than max attempts, we should
      // schedule the job to run again later.
      context.CandidateState = new ScheduledState(delay)
      {
        Reason = String.Format(
              "Retry attempt {0} of {1}: {2}",
              retryAttempt,
              Attempts,
              exceptionMessage.Length > maxMessageLength
              ? exceptionMessage.Substring(0, maxMessageLength - 1) + "…"
              : exceptionMessage)
      };

     if (LogEvents)
      {
        Logger.WarnException(
                  $"Failed to process the job '{context.BackgroundJob.Id}': an exception occurred. Retry attempt {retryAttempt} of {Attempts} will be performed in {delay}.",
                  failedState.Exception);
      }
    }

    /// <summary>
    /// Transition the candidate state to the deleted state.
    /// </summary>
    /// <param name="context">The state context.</param>
    /// <param name="failedState">Object which contains details about the current failed state.</param>
    private void TransitionToDeleted(ElectStateContext context, FailedState failedState)
    {
      context.CandidateState = new DeletedState
      {
        Reason = "Exceeded the maximum number of retry attempts."
      };

      Console.WriteLine(String.Format(
                "Failed to process the job '{0}': an exception occured. Job was automatically deleted because the retry attempt count exceeded {1}.",
                context.BackgroundJob.Id,
                Attempts));

      if (LogEvents)
      {
        Logger.WarnException(
                    $"Failed to process the job '{context.BackgroundJob.Id}': an exception occured. Job was automatically deleted because the retry attempt count exceeded {Attempts}.",
                    failedState.Exception);
      }
    }

    // delayed_job uses the same basic formula
    private static int SecondsToDelay(long retryCount)
    {
      var random = new Random();
      return (int)Math.Round(
          Math.Pow(retryCount - 1, 4) + 15 + (random.Next(30) * (retryCount)));
    }
  }
}
