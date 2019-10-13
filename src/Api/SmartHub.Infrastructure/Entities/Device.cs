using System;

namespace SmartHub.Infrastructure.Entities
{
  public class Device
  {
    public int Id { get; set; }

    public Guid DeviceId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Guid? UserId { get; set; }

  }
}
