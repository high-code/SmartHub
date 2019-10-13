using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartHub.Identity.Identity;
using SmartHub.Identity.Models;

namespace SmartHub.Identity.Controllers
{
  public class RegistrationController : Controller
  {
    private readonly UserManager<ApplicationUser> _userManager;

    public RegistrationController(UserManager<ApplicationUser> userManager)
    {
      _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody]RegistrationViewModel viewModel)
    {
      if (ModelState.IsValid)
      {
        var user = new ApplicationUser()
        {
          UserName = viewModel.UserName,
          Email = viewModel.Email,
          FullName = viewModel.FullName,
        };

        var result = await _userManager.CreateAsync(user, viewModel.Password);

        if (result.Succeeded)
          return Ok();

        return BadRequest(result.Errors);
      }

      return BadRequest(ModelState);
    }
  }
}
