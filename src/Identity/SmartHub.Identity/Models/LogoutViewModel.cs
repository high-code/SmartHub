using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHub.Identity.Models
{
  public class LogoutViewModel
  {
     public string LogoutId { get; set; }

     public bool ShowSignoutPrompt { get; set; }
  }
}
