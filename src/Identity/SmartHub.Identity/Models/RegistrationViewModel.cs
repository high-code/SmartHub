using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartHub.Identity.Models
{
  public class RegistrationViewModel
  {
    [Required]
    [StringLength(100, ErrorMessage = "The {0} should be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DisplayName("Username")]
    public string UserName { get; set; }


    [Required]
    [StringLength(100, ErrorMessage = "The {0} should be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DisplayName("FullName")]
    public string FullName { get; set; }

    [Required]
    [EmailAddress]
    [DisplayName("Email")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [DisplayName("Password")]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password",ErrorMessage = "Confirmation password should match with password")]
    [DisplayName("Confirmation password")]
    public string RepeatedPassword { get; set; }

  }
}
