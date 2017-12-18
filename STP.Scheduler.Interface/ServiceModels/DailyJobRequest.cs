using System.ComponentModel.DataAnnotations;

namespace STP.Scheduler.Interface.ServiceModels
{
  public class DailyJobRequest
  {
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
