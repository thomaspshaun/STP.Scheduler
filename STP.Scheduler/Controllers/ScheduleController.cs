using Microsoft.AspNetCore.Mvc;
using STP.Scheduler.Interface.Contracts;
using STP.Scheduler.Interface.ServiceModels;

namespace STP.Scheduler.Controllers
{
  [Produces("application/json")]
  [Route("api/Schedule")]
  public class ScheduleController : Controller
  {
    private readonly IJobService _jobService; 

    public ScheduleController(IJobService jobService)
    {
      _jobService = jobService; 
    }

    [HttpPost]
    [Route("CreateADailyJob")]
    public JobReponse CreateADailyJob([FromBody]DailyJobRequest dailyJobRequest)
    {
      return _jobService.CreateDailyJob(dailyJobRequest); 
    }

    [HttpPost]
    [Route("CreateAHourlyJob")]
    public JobReponse CreateAHourlyJob([FromBody]HourlyJobRequest dailyJobRequest)
    {
      return _jobService.CreateHourlyJob(dailyJobRequest);
    }
    [HttpPost]
    [Route("CreateAMonthlyJob")]
    public JobReponse CreateAMonthlyJob([FromBody]MonthlyJobRequest dailyJobRequest)
    {
      return _jobService.CreateMonthlyJob(dailyJobRequest);
    }
    [HttpPost]
    [Route("CreateAWeeklyJob")]
    public JobReponse CreateAWeeklyJob([FromBody]WeeklyJobRequest dailyJobRequest)
    {
      return _jobService.CreateWeeklyJob(dailyJobRequest);
    }
  }
}