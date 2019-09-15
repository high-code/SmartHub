using System;
using System.ComponentModel.DataAnnotations;

namespace SmartHub.Identity.Models
{
  public class LoginViewModel
  {
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }

    public bool RememberLogin { get; set; }

    public string ReturnUrl { get; set; }
  }
}
