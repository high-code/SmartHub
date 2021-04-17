using SmartHub.Infrastructure.Entities;
using SmartHub.DataAccess.Contracts;
namespace SmartHub.Infrastructure.Contracts
{
  public interface IDeviceRepository : IRepository<SmartHubContext, Device>
  {

  }
}
