using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace SmartHub.SPA.Controllers
{
  [Route("api/[controller]")]
  public class SettingController : Controller
  {

    private readonly IOptionsSnapshot<AppSettings> _snapshot;


    public SettingController(IOptionsSnapshot<AppSettings> snapshot)
    {
      _snapshot = snapshot;
    }


    [HttpGet]
    public JsonResult GetSettings()
    {
      return new JsonResult(_snapshot.Value);
    }

    

  }
}
