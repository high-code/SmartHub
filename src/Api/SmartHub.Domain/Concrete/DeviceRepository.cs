using System;
using System.Collections.Generic;
using SmartHub.Domain.Contracts;
using SmartHub.Domain.Entities;

namespace SmartHub.Domain.Concrete
{
  public class DeviceRepository : Repository<Device>, IDeviceRepository
  {
    public DeviceRepository(SmartHubContext context) : base(context)
    { }

    public SmartHubContext SmartHubContext => DbContext as SmartHubContext;
  }
}
