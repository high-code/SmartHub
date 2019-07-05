using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace SmartHub.SPA.Controllers
{
  [Route("api/[controller]")]
  public class ConfigurationController : Controller
  {

    private readonly IOptionsSnapshot<AppSettings> _snapshot;


    public ConfigurationController(IOptionsSnapshot<AppSettings> snapshot)
    {
      _snapshot = snapshot;
    }


    [HttpGet]
    [Route("configuration")]
    public JsonResult GetConfiguration()
    {
      return new JsonResult(_snapshot.Value);
    }

    

  }
}
