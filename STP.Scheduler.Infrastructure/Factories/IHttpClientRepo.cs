using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STP.Scheduler.Infrastructure.Factories
{
  public interface IHttpClientRepo
  {
    Res PostToWebApi<Req, Res>(Req req, string serviceUrl, string action, string controller) where Res : new();

    void PostToWebApi<Req>(Req req, string serviceUrl, string action, string controller);

    void PostToWebApi(string serviceUrl, string action, string controller); 
  }
}
