using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STP.Scheduler.Interface.Contracts
{
  public interface IJobRepo
  {

    void CallWebServiceMethod(string serviceUrl, string controller, string action);
  }
}
