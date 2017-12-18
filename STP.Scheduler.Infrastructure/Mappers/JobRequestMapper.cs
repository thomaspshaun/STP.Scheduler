using STP.Scheduler.Domain.DomainModels;
using STP.Scheduler.Interface.ServiceModels;

namespace STP.Scheduler.Infrastructure.Mappers
{
  public class JobRequestMapper
  {
    public JobRequest MapDailyJobRequest(DailyJobRequest request)
    {
      return new JobRequest()
      {
        JobName = "daily_"+request.Controller + "_" + request.Action+"_"+"d0"+"h"+request.Hour+"m"+request.Minute,
        Action = request.Action,
        Controller = request.Controller,
        Day = 0,
        Hour = request.Hour,
        Minute = request.Minute,
        ServiceUrl = request.ServiceUrl
      }; 
    }

    public JobRequest MapWeeklyJobRequest(WeeklyJobRequest request)
    {
      return new JobRequest()
      {
        JobName = "weekly_"+request.Controller + "_" + request.Action + "_" + "d"+ request.Day + "h" + request.Hour + "m" + request.Minute,
        Action = request.Action,
        Controller = request.Controller,
        Day = request.Day,
        Hour = request.Hour,
        Minute = request.Minute,
        ServiceUrl = request.ServiceUrl
      };
    }


    public JobRequest MapHourlyJobRequest(HourlyJobRequest request)
    {
      return new JobRequest()
      {
        JobName = "hourly_"+request.Controller + "_" + request.Action + "_" + "d0" + "h0" + "m" + request.Minute,

        Action = request.Action,
        Controller = request.Controller,
        Day = 0,
        Hour = 0,
        Minute = request.Minute,
        ServiceUrl = request.ServiceUrl
      };
    }



    public JobRequest MapMonthlyJobRequest(MonthlyJobRequest request)
    {
      return new JobRequest()
      {
        JobName = "monthly_"+request.Controller + "_" + request.Action + "_" + "d0" + "h" + request.Hour + "m" + request.Minute,

        Action = request.Action,
        Controller = request.Controller,
        Day = request.Day,
        Hour = request.Hour,
        Minute = request.Minute,
        ServiceUrl = request.ServiceUrl
      };
    }
  }
}
