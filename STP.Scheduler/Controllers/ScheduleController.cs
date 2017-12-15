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
    public void CreateADailyJob([FromBody]DailyJobRequest dailyJobRequest)
    {
      _jobService.CreateDailyJob(dailyJobRequest); 
    }
  }
}