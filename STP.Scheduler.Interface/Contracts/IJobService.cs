using STP.Scheduler.Interface.ServiceModels;

namespace STP.Scheduler.Interface.Contracts
{
  public interface IJobService
  {
    void CreateDailyJob(DailyJobRequest dailyJobRequest); 
  }
}
