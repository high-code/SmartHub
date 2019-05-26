using System;

namespace SmartHub.Models
{
  public class DeviceModel
  {
    public int Id { get; set; }

    public Guid DeviceId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
  }
}
