using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace SmartHub.SPA.Controllers
{
  [Route("[controller]")]
  public class ConfigurationController : Controller
  {

    private readonly IOptionsSnapshot<AppSettings> _snapshot;

    public ConfigurationController(IOptionsSnapshot<AppSettings> snapshot)
    {
      _snapshot = snapshot;
    }

    [HttpGet]
    public JsonResult GetConfiguration()
    {
      return new JsonResult(_snapshot.Value);
    }

    

  }
}
