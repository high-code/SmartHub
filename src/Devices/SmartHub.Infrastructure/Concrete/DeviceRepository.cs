using SmartHub.Infrastructure.Contracts;
using SmartHub.Infrastructure.Entities;
using SmartHub.DataAccess.Concrete;
namespace SmartHub.Infrastructure.Concrete
{
  public class DeviceRepository : Repository<Device>, IDeviceRepository
  {
    public DeviceRepository(SmartHubContext context) : base(context)
    { }

    public SmartHubContext SmartHubContext => DbContext as SmartHubContext;
  }
}
