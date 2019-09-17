﻿using System;
using System.Threading.Tasks;
using IdentityServer4.Services;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartHub.Identity.Identity;
using SmartHub.Identity.Models;

namespace SmartHub.Identity.Controllers
{
  public class AccountController : Controller
  {
    private readonly IIdentityServerInteractionService _identityServerInteractionService;
    private readonly TestUserStore _users;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(IIdentityServerInteractionService identityServerInteractionService,
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    TestUserStore users = null)
    {
      _identityServerInteractionService = identityServerInteractionService;
      _users = users ?? new TestUserStore(Config.GetTestUsers());
      _userManager = userManager;
      _signInManager = signInManager;
    }

    public IActionResult Login(string returnUrl)
    {
      return View(new LoginViewModel {ReturnUrl = returnUrl});
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel viewModel)
    {

      var context = _identityServerInteractionService.GetAuthorizationContextAsync(viewModel.ReturnUrl);

      if (ModelState.IsValid)
      {
        var signInResult = await _signInManager.PasswordSignInAsync(viewModel.UserName, viewModel.Password, true, false);
        
        if(signInResult.Succeeded)
        {

          if (context != null)
          {
            return Redirect(viewModel.ReturnUrl);
          }

          if (Url.IsLocalUrl(viewModel.ReturnUrl))
            return Redirect(viewModel.ReturnUrl);
          if (string.IsNullOrEmpty(viewModel.ReturnUrl))
            return Redirect("~/");

          throw new Exception("Invalid return url");
        }

        ModelState.AddModelError(string.Empty, "Invalid credentials");
      }

      return View(viewModel);
    }


    [HttpGet]
    public async Task<IActionResult> Logout(string logoutId)
    {
      var vm = new LogoutViewModel() {LogoutId = logoutId};

      if (User?.Identity.IsAuthenticated != true)
        return View(vm);

      var context = await _identityServerInteractionService.GetLogoutContextAsync(logoutId);

      vm.ShowSignoutPrompt = context?.ShowSignoutPrompt ?? false;

      return View(vm);

    }

    [HttpPost]
    public async Task<IActionResult> Logout(LogoutViewModel model)
    {
      var logout = await _identityServerInteractionService.GetLogoutContextAsync(model.LogoutId);

      var vm = new LoggedOutViewModel
      {
        LogoutId = model.LogoutId,
        PostLogoutRedirectUri = logout?.PostLogoutRedirectUri,
        SignoutIframeUrl = logout?.SignOutIFrameUrl,
        ClientName = string.IsNullOrWhiteSpace(logout?.ClientName) ? logout?.ClientId : logout?.ClientName
      };

      if (User?.Identity.IsAuthenticated == true)
      {
        await HttpContext.SignOutAsync();
      }

      return View("loggedOut", vm);
    }
  }
}
