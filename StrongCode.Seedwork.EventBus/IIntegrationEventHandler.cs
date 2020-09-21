using System;
using System.Threading.Tasks;

namespace StrongCode.Seedwork.EventBus
{
  public interface IIntegrationEventHandler<in T>: IDisposable where T: IntegrationEvent
  {
    Task Handle(T @event);
  }
}
