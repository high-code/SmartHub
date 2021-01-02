using MediatR;
using System.Runtime.Serialization;

namespace SmartHub.Devices.Application.Commands
{

  [DataContract]
  public class DeleteDeviceCommand : IRequest<bool>
  {
    public int DeviceId { get; set; }
  }
}
