using MediatR;
using System.Runtime.Serialization;

namespace SmartHub.Devices.Application.Commands
{

  [DataContract]
  public class RegisterDeviceCommand : IRequest<bool>
  {
    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public string Description { get; set; }
  }
}
