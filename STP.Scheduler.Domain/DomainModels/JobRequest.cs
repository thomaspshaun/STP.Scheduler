using System;
using System.Collections.Generic;
using System.Text;

namespace STP.Scheduler.Domain.DomainModels
{
  public class JobRequest
  {
    public string JobName { get; set; }
    public DayOfWeek Day { get; set; }
    public int Hour { get; set; }
    public int Minute { get; set; }
    public string ServiceUrl { get; set; }
    public string Controller { get; set; }
    public string Action { get; set; }
  }
}
