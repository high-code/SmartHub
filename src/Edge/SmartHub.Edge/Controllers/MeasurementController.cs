using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartHub.Edge.Application.Commands;

namespace SmartHub.Edge.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class MeasurementController : ControllerBase
  {

    private readonly IMediator _mediator;
    private readonly ILogger<MeasurementController> _logger;
    public MeasurementController(IMediator mediator, ILogger<MeasurementController> logger)
    {
      _mediator = mediator;
      _logger = logger;
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
