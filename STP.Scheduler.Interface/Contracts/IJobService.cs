using STP.Scheduler.Interface.ServiceModels;

namespace STP.Scheduler.Interface.Contracts
{
  public interface IJobService
  {
    JobReponse CreateDailyJob(DailyJobRequest dailyJobRequest);
    JobReponse CreateHourlyJob(HourlyJobRequest dailyJobRequest);
    JobReponse CreateMonthlyJob(MonthlyJobRequest dailyJobRequest);
    JobReponse CreateWeeklyJob(WeeklyJobRequest dailyJobRequest); 
  }
}
