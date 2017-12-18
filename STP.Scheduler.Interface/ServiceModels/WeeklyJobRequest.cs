using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STP.Scheduler.Interface.ServiceModels
{
  public class WeeklyJobRequest
  {
    public DayOfWeek Day { get; set; }
    public int Hour { get; set; }
    public int Minute { get; set; }
    public string ServiceUrl { get; set; }
    public string Controller { get; set; }
    public string Action { get; set; }
  }
}
