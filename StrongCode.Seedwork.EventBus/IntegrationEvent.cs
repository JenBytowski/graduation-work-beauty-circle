using System;
using Newtonsoft.Json;

namespace StrongCode.Seedwork.EventBus
{
  public class IntegrationEvent
  {
    public IntegrationEvent()
    {
      EventId = Guid.NewGuid();
      CreationDate = DateTime.UtcNow;
    }

    [JsonConstructor]
    public IntegrationEvent(Guid eventId, DateTime createDate)
    {
      EventId = eventId;
      CreationDate = createDate;
    }

    [JsonProperty]
    public Guid EventId { get; private set; }

    [JsonProperty]
    public DateTime CreationDate { get; private set; }
  }
}
