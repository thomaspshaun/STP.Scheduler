using System;
using System.ComponentModel.DataAnnotations;

namespace STP.Scheduler.Interface.ServiceModels
{
  public class MonthlyJobRequest
  {
    public DayOfWeek Day { get; set; }
    public int Hour { get; set; }
    public int Minute { get; set; }
    [Required]
    public string ServiceUrl { get; set; }
    [Required]
    public string Controller { get; set; }
    [Required]
    public string Action { get; set; }
  }
}
