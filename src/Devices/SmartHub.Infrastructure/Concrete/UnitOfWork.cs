using SmartHub.Infrastructure.Contracts;
using SmartHub.DataAccess.Concrete;

namespace SmartHub.Infrastructure.Concrete
{
  public class UnitOfWork : UnitOfWorkBase<SmartHubContext>,IUnitOfWork
  {

    public IDeviceRepository Devices { get; set; }

    public UnitOfWork(SmartHubContext context) : base(context)
    {
      Devices = new DeviceRepository(_context);
    }
  }
}
