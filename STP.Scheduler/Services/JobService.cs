using Hangfire;
using STP.Scheduler.Interface.Contracts;
using STP.Scheduler.Interface.ServiceModels;
using Hangfire.Storage;
using STP.Scheduler.Infrastructure.Mappers;
using STP.Scheduler.Domain.DomainModels;

namespace STP.Scheduler.Services
{

  public class JobService : IJobService
  {
    private readonly IJobRepo _jobRepo;

    private JobRequestMapper jobRequestMapper = new JobRequestMapper(); 

    public JobService(IJobRepo jobRepo)
    {
      _jobRepo = jobRepo;
    }
    public JobReponse CreateDailyJob(DailyJobRequest request)
    {
      JobRequest jobRequest = jobRequestMapper.MapDailyJobRequest(request);
      JobReponse response = new JobReponse(); 

      if (!DoesJobExist(jobRequest.JobName))
      {
        RecurringJob.AddOrUpdate(recurringJobId: jobRequest.JobName,
                                 methodCall: () => _jobRepo.CallWebServiceMethod(jobRequest.ServiceUrl, jobRequest.Controller, jobRequest.Action),
                                 cronExpression: Cron.Daily(jobRequest.Hour, jobRequest.Minute),
                                 timeZone: null,
                                 queue: "Daily"
                                );
        response.Result = "Job Added";
      }
      else
      {
        response.Result = "Job Already Exists : " + jobRequest.JobName;
      }
      return response; 
    }



    public JobReponse CreateHourlyJob(HourlyJobRequest request)
    {
      JobRequest jobRequest = jobRequestMapper.MapHourlyJobRequest(request);
      JobReponse response = new JobReponse();
      if (!DoesJobExist(jobRequest.JobName))
      {
        RecurringJob.AddOrUpdate(recurringJobId: jobRequest.JobName,
                                 methodCall: () => _jobRepo.CallWebServiceMethod(jobRequest.ServiceUrl, jobRequest.Controller, jobRequest.Action),
                                 cronExpression: Cron.Hourly(jobRequest.Minute),
                                 timeZone: null,
                                 queue: "Hourly"
                           );
        response.Result = "Job Added";
      }
      else
      {
        response.Result = "Job Already Exists : " + jobRequest.JobName;
      }
      return response;

    }

    public JobReponse CreateMonthlyJob(MonthlyJobRequest request)
    {
      JobRequest jobRequest = jobRequestMapper.MapMonthlyJobRequest(request);
      JobReponse response = new JobReponse();
      if (!DoesJobExist(jobRequest.JobName))
      {
        RecurringJob.AddOrUpdate(recurringJobId: jobRequest.JobName,
                                 methodCall: () => _jobRepo.CallWebServiceMethod(jobRequest.ServiceUrl, jobRequest.Controller, jobRequest.Action),
                                 cronExpression: Cron.Monthly((int)jobRequest.Day, jobRequest.Hour, jobRequest.Minute),
                                 timeZone: null,
                                 queue: "Monthly"
                              );
        response.Result = "Job Added";
      }
      else
      {
        response.Result = "Job Already Exists : " + jobRequest.JobName;
      }
      return response;

    }

    public JobReponse CreateWeeklyJob(WeeklyJobRequest request)
    {
      JobRequest jobRequest = jobRequestMapper.MapWeeklyJobRequest(request);
      JobReponse response = new JobReponse();
      if (!DoesJobExist(jobRequest.JobName))
      {
        RecurringJob.AddOrUpdate(recurringJobId: jobRequest.JobName,
                                 methodCall: () => _jobRepo.CallWebServiceMethod(jobRequest.ServiceUrl, jobRequest.Controller, jobRequest.Action),
                                 cronExpression: Cron.Weekly(jobRequest.Day, jobRequest.Hour, jobRequest.Minute),
                                 timeZone: null,
                                 queue: "Weekly"
                        );
        response.Result = "Job Added";
      }
      else
      {
        response.Result = "Job Already Exists : " + jobRequest.JobName; 
      }
      return response;

    }
    private static bool DoesJobExist(string JobID)
    {
      RecurringJobDto existingJob;
      using (var connection = JobStorage.Current.GetConnection())
      {
        var existingJobs = connection.GetRecurringJobs();
        existingJob = existingJobs.Find(s => s.Id == JobID);
      }

      return existingJob != null;
    }
  }
}
