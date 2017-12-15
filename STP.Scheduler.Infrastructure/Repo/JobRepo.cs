using STP.Scheduler.Infrastructure.Factories;
using STP.Scheduler.Interface.Contracts;

namespace STP.Scheduler.Infrastructure.Repo
{
  public class JobRepo : IJobRepo

  {
    private readonly IHttpClientRepo _httpClientRepo;

    public JobRepo(IHttpClientRepo httpClientRepo)
    {
      _httpClientRepo = httpClientRepo; 
    }
    [AutomaticRetry(Attempts = 1, LogEvents = true, OnAttemptsExceeded = Hangfire.AttemptsExceededAction.Fail)]
    public void CallWebServiceMethod(string serviceUrl, string controller, string action  )
    {
      _httpClientRepo.PostToWebApi(serviceUrl, action, controller);
    }
  }
}
