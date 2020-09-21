using System;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;

namespace StrongCode.Seedwork.EventBus.RabbitMQ
{
  class Subscription
  {
    public Type EventType { get; private set; }
    public Type HandlerType { get; private set; }

    public string ConsumerTag { get; set; }

    public Func<BasicDeliverEventArgs, Task> HandleAction { get; private set; }

    public static Subscription CreateNew<T, TH>(string consumerTag)
    {
      return new Subscription {EventType = typeof(T), HandlerType = typeof(TH), ConsumerTag = consumerTag};
    }

    public bool Check<TEvent, THandler>()
    {
      return typeof(TEvent) == EventType && typeof(THandler) == HandlerType;
    }
  }
}
