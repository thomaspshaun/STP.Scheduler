using Hangfire;
using STP.Scheduler.Interface.Contracts;
using STP.Scheduler.Interface.ServiceModels;
using STP.Scheduler.Infrastructure.Repo; 


namespace STP.Scheduler.Services
{

  public class JobService : IJobService
  {
    //private static Logger logger = LogManager.GetCurrentClassLogger();
    private readonly IJobRepo _jobRepo;

    public JobService(IJobRepo jobRepo)
    {
      _jobRepo = jobRepo;
    }
    public void CreateDailyJob(DailyJobRequest request)
    {
      RecurringJob.AddOrUpdate(recurringJobId: request.JobName,
                               methodCall: () => _jobRepo.CallWebServiceMethod(request.ServiceUrl, request.Controller, request.Action),
                               cronExpression: Cron.Daily(request.HourOfDay),
                               timeZone: null,
                               queue: "scheduler"
                              );
    }

  }
}
