﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace STP.Scheduler.Controllers
{
    [Produces("application/json")]
    [Route("api/Test")]
    public class TestController : Controller
    {

    [HttpPost]
    [Route("Test")]
    public string Test()
    {
      return "bla blab albs";
    }
  }
}