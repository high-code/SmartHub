using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Configuration;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Mvc;
using SmartHub.Identity.Models;
using SmartHub.DataAccess.Contracts;
using SmartHub.Identity.Infrastructure;
using SmartHub.Identity.Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;

namespace SmartHub.Identity.Controllers
{

  [Authorize]
  public class DeviceController : Controller
  {
    private readonly IRepository<IdentityServerDbContext, IdentityServerClient> _repository;
    private readonly IDeviceFlowInteractionService _deviceFlowInteractionService;

    public DeviceController(IDeviceFlowInteractionService deviceFlowInteractionService, 
      IRepository<IdentityServerDbContext, IdentityServerClient> repository)
    {
      _deviceFlowInteractionService = deviceFlowInteractionService;
      _repository = repository;

    }
    // GET: /<controller>/
    [HttpGet]
    public async Task<IActionResult> Index()
    {

      string userCode = Request.Query["userCode"];

      if (string.IsNullOrWhiteSpace(userCode)) return View("UserCodeCapture");

      var vm = await CreateViewModel(userCode);
      vm.ConfirmUserCode = true;
      return View("UserCodeConfirmation", vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UserCodeCapture(string userCode)
    {
      if(string.IsNullOrWhiteSpace(userCode)) return View("Error", "User code couldn't be empty");
      var vm = await CreateViewModel(userCode);
      vm.ConfirmUserCode = false;
      return View("UserCodeConfirmation", vm);
    }

    private async Task<DeviceAuthorizationViewModel> CreateViewModel(string userCode)
    {
      DeviceFlowAuthorizationRequest f = await _deviceFlowInteractionService.GetAuthorizationContextAsync(userCode);

      var device = _repository.Find(d => d.ClientId == f.ClientId).Single();

      if (device.UserId.ToString() != User.GetSubjectId())
      {
        throw new ArgumentException("This user can't authorize this device");
      }

      return new DeviceAuthorizationViewModel()
      {
        UserCode = userCode,
        ClientId = device.ClientId,
        ClientName = device.ClientName
      };
    }
    public async Task<IActionResult> Callback(DeviceAuthorizationViewModel viewModel)
    {
      var request = await _deviceFlowInteractionService.GetAuthorizationContextAsync(viewModel.UserCode);
      if (request == null) return View("Error");

      if(viewModel.Button == "no")
      {
        return View("Error");
      }
      
      if(viewModel.Button == "yes")
      {
        var grantedConsent = new ConsentResponse()
        {
          RememberConsent = true,
          ScopesConsented = request.ScopesRequested
        };

        await _deviceFlowInteractionService.HandleRequestAsync(viewModel.UserCode, grantedConsent);
      }

      return View("Success", "Successfully authorized device");
    }
  }
}
