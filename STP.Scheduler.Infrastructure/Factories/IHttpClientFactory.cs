using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace STP.Scheduler.Infrastructure.Factories
{
  public interface IHttpClientFactory : IDisposable
  {
    HttpClient Create(string baseAddress);
  }
}
