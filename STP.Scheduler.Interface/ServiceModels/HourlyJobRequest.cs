using System.ComponentModel.DataAnnotations;

namespace STP.Scheduler.Interface.ServiceModels
{
  public class HourlyJobRequest
  {
    public int Minute { get; set; }
    [Required]
    public string ServiceUrl { get; set; }
    [Required]
    public string Controller { get; set; }
    [Required]
    public string Action { get; set; }
  }
}
