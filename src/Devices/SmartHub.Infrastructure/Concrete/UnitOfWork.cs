using SmartHub.Infrastructure.Contracts;
using SmartHub.DataAccess.Concrete;

namespace SmartHub.Infrastructure.Concrete
{
  public class UnitOfWork : UnitOfWorkBase<SmartHubContext>,IUnitOfWork
  {

    public IDeviceRepository Devices { get; }

    public UnitOfWork(SmartHubContext context, IDeviceRepository deviceRepository) : base(context)
    {
      Devices = deviceRepository;
    }
  }
}
