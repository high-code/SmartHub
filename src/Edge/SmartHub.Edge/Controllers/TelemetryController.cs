using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartHub.Edge.Models;

namespace SmartHub.Edge.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TelemetryController : ControllerBase
  {
    
    [HttpPost]
    public IActionResult Post([FromBody] Telemetry telemetry)
    {


      return Ok();
    }

  }
}
