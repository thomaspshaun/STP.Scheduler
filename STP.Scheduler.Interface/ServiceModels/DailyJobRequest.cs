using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STP.Scheduler.Interface.ServiceModels
{
  public class DailyJobRequest
  {
    public string JobName { get; set; }
    public int HourOfDay { get; set; }
    public string ServiceUrl { get; set; }
    public string Controller { get; set; }
    public string Action { get; set; }
  }
}
