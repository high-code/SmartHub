using Newtonsoft.Json;
using System;

namespace SmartHub.Messaging.Events
{
  public class IntegrationEvent
  {
    public IntegrationEvent()
    {
      Id = Guid.NewGuid();
      CreateDate = DateTime.UtcNow;
    }

    [JsonConstructor]
    public IntegrationEvent(Guid id, DateTime createDate)
    {
      Id = id;
      CreateDate = createDate;
    }

    [JsonProperty]
    public Guid Id { get; private set; }

    [JsonProperty]
    public DateTime CreateDate { get; private set; }


  }
}
