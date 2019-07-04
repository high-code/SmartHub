using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartHub.Edge.Contracts;
using SmartHub.Edge.Models;

namespace SmartHub.Edge.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TelemetryController : ControllerBase
  {
    private readonly INotificationService _notificationService;

    public TelemetryController(INotificationService notificationService)
    {
      _notificationService = notificationService;
    }
    
    [HttpPost]
    public void Post([FromBody] Telemetry telemetry)
    {
        
      _notificationService.Save(telemetry);
      
    }

  }
}
