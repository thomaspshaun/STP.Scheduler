using NLog;
using System;
using System.Collections.Generic;

namespace STP.Scheduler.Infrastructure.Logging
{
  public class LoggerService : ILoggerService
  {
    private ILogger _logger;

    public LoggerService()
    {
      _logger = LogManager.GetLogger(this.GetType().FullName);
    }

    /// <summary>
    /// Log debug information
    /// </summary>
    /// <param name="debug"></param>
    public void LogDebug(string debug)
    {
      var args = GetLogArguments();

      _logger.Debug(debug, args);
    }

    /// <summary>
    /// Log general information
    /// </summary>
    /// <param name="info"></param>
    public void LogInformation(string info)
    {
      var args = GetLogArguments();

      _logger.Info(info, args);
    }

    /// <summary>
    /// Log an error
    /// </summary>
    /// <param name="ex"></param>
    /// <param name="message"></param>
    public void LogError(Exception ex, string message)
    {
      var args = GetLogArguments();

      _logger.Error(ex, message, args);
    }

    /// <summary>
    /// Get all the arguments that we want to send to our log provider
    /// </summary>
    /// <returns></returns>
    private object[] GetLogArguments()
    {
      var resultList = new List<object>();

      return resultList.ToArray();
    }
  }
}
