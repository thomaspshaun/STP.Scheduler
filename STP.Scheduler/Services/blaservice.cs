using STP.Scheduler.Interface.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STP.Scheduler.Services
{
  public class blaservice
  {
    private readonly IJobRepo _jobRepo;

    public blaservice(IJobRepo jobRepo)
    {
      _jobRepo = jobRepo;
    }

    public void bla()
    {
      _jobRepo.CallWebServiceMethod("", "", "");
    }
  }
}
