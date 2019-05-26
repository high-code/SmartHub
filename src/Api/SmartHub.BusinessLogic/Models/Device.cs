using System;

namespace SmartHub.BusinessLogic.Models
{
  public class Device
  {
    public int Id { get; set; }

    public Guid DeviceId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int UserId { get; set; }
  }
}
