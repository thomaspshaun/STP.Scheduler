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
        JobName = "DailyJob_"+request.Controller + "_" + request.Action+"_"+"D0"+"H"+request.Hour+"M"+request.Minute,
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
        JobName = request.Controller + "_" + request.Action + "_" + "D"+ request.Day + "H" + request.Hour + "M" + request.Minute,
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
        JobName = request.Controller + "_" + request.Action + "_" + "D0" + "H0" + "M" + request.Minute,

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
        JobName = request.Controller + "_" + request.Action + "_" + "D0" + "H" + request.Hour + "M" + request.Minute,

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
