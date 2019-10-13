using SmartHub.Domain;
using SmartHub.Domain.Contracts;
using SmartHub.Infrastructure.Contracts;

namespace SmartHub.Infrastructure.Concrete
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly SmartHubContext _context;

    public IDeviceRepository Devices { get; set; }

    public IMeasurementRepository Measurements { get; set; }

    public UnitOfWork(SmartHubContext context)
    {
      _context = context;
      // Possible place to use Property Injection
      Devices = new DeviceRepository(_context);
      Measurements = new MeasurementRepository(_context);
    }


    public int Commit() => _context.SaveChanges();

    public void Dispose()
    {
      _context.Dispose();
    }
  }
}
