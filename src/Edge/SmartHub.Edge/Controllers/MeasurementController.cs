using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartHub.Edge.Application.Commands;
using SmartHub.Edge.Domain;

namespace SmartHub.Edge.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class MeasurementController : ControllerBase
  {

    private readonly IMediator _mediator;
    private readonly ILogger<MeasurementController> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public MeasurementController(IMediator mediator, ILogger<MeasurementController> logger, IUnitOfWork unitOfWork)
    {
      _mediator = mediator;
      _logger = logger;
      _unitOfWork = unitOfWork;
    }

    [HttpGet("{deviceId}")]
    public IActionResult Get(Guid deviceId)
    {
      _logger.LogInformation("GET measurements for deviceId={deviceId}", deviceId);

      var measurements = _unitOfWork.Measurements.Find(m => m.DeviceId == deviceId).ToList();

      return Ok(measurements);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] RecordMeasurementsCommand recordMeasurementsCommand)
    {

      _logger.LogInformation("Received {command} command. Start handling", recordMeasurementsCommand);

      var result = await _mediator.Send(recordMeasurementsCommand);

      if(!result)
      {
        return BadRequest();
      } 

      return Ok();
    }

  }
}
