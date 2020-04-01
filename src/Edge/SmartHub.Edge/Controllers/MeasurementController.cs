using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartHub.Edge.Application.Commands;

namespace SmartHub.Edge.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class MeasurementController : ControllerBase
  {

    private readonly IMediator _mediator;

    public MeasurementController(IMediator mediator)
    {
      _mediator = mediator;
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] RecordMeasurementsCommand recordMeasurementsCommand)
    {

      var result = await _mediator.Send(recordMeasurementsCommand);

      if(!result)
      {
        return BadRequest();
      } 

      return Ok();
    }

  }
}
