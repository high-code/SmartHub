using System;

namespace SmartHub.Domain.Entities
{
  public class Device
  {
    public int Id { get; set; }

    public Guid DeviceId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
 
  }
}
