using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace StrongCode.Seedwork.EventBus.RabbitMQ
{
  public class RabbitMqEventBus : IEventBus, IDisposable
  {
    private readonly string HOST;
    private readonly string PORT;
    private readonly string USER_NAME;
    private readonly string PASSWORD;
    private readonly string EXCHANGE_NAME;

    private readonly IServiceProvider _serviceProvider;

    private IConnection _connection = null;
    private IModel _consumeChannel = null;
    private string _consumerTag = null;

    private List<Subscription> _subscriptions = new List<Subscription>();

    public RabbitMqEventBus
    (
      string host,
      string port,
      string userName,
      string password,
      string exchangeName,
      IServiceProvider serviceProvider
    )
    {
      this.HOST = host;
      this.PORT = port;
      this.USER_NAME = userName;
      this.PASSWORD = password;
      this.EXCHANGE_NAME = exchangeName;
      this._serviceProvider = serviceProvider;
    }

    private static IConnection CreateConnection(string hostName,string port, string userName, string password)
    {
      var factory = new ConnectionFactory() {HostName = hostName, Port = int.Parse(port),UserName = userName, Password = password};
      return factory.CreateConnection();
    }

    private IConnection GetConnection()
    {
      var connectionBroken =
        this._connection == null ||
        !this._connection.IsOpen;

      if (connectionBroken)
      {
        this._connection = CreateConnection(this.HOST, this.PORT, this.USER_NAME, this.PASSWORD);
      }

      return this._connection;
    }


    private static IModel CreateChannel(IConnection connection)
    {
      return connection.CreateModel();
    }

    private IModel GetConsumeChannel()
    {
      return this._consumeChannel ??= CreateChannel(this.GetConnection());
    }

    private static void EnsureExchangeExists(IModel channel, string exchangeName)
    {
      channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, true, false);
    }

    private static void EnsureQueueExists(IModel channel, string queueName)
    {
      if (String.IsNullOrEmpty(queueName))
      {
        throw new ApplicationException(
          "Queue name should be specified if you are not just publishing messages but consuming them too");
      }

      channel.QueueDeclare(queueName, true, false, false, null);
    }

    private static byte[] Serialize(IntegrationEvent @event)
    {
      var asJson = JsonConvert.SerializeObject(@event);
      var asBytes = Encoding.UTF8.GetBytes(asJson);
      return asBytes;
    }

    private static T Deserialize<T>(byte[] bodyAsBytes)
    {
      var bodyAsString = Encoding.UTF8.GetString(bodyAsBytes);
      var body = JsonConvert.DeserializeObject<T>(bodyAsString);
      return body;
    }

    private static string GetEventRoutingKey<TEvent>()
    {
      return typeof(TEvent).Name;
    }

    private static string GetQueueName<THandler>()
    {
      return $"{typeof(THandler).FullName}.ReceiveQueue";
    }

    private static string GetEventRoutingKey(IntegrationEvent @event)
    {
      return @event.GetType().Name;
    }

    private void Unsubscribe(Subscription subscription)
    {
      var channel = this.GetConsumeChannel();
      channel.BasicCancel(subscription.ConsumerTag);
      this._subscriptions.Remove(subscription);
    }

    public void Publish(IntegrationEvent @event)
    {
      var connection = this.GetConnection();
      using var channel = CreateChannel(connection);

      EnsureExchangeExists(channel, this.EXCHANGE_NAME);

      channel.BasicPublish
      (
        exchange: this.EXCHANGE_NAME,
        routingKey: GetEventRoutingKey(@event),
        basicProperties: null,
        body: Serialize(@event)
      );
    }

    public void Subscribe<TEvent, THandler>() where TEvent : IntegrationEvent
      where THandler : IIntegrationEventHandler<TEvent>
    {
      if (this._subscriptions.Any(s => s.Check<TEvent, THandler>()))
      {
        return;
      }

      var channel = this.GetConsumeChannel();
      var queueName = GetQueueName<THandler>();

      EnsureExchangeExists(channel, this.EXCHANGE_NAME);
      EnsureQueueExists(channel, queueName);

      channel.QueueBind(queueName, this.EXCHANGE_NAME, GetEventRoutingKey<TEvent>(), null);

      var consumer = new EventingBasicConsumer(channel);

      consumer.Received += async (sender, args) =>
      {
        using var handler = this._serviceProvider.GetService(typeof(THandler)) as IIntegrationEventHandler<TEvent>;

        try
        {
          await handler.Handle(Deserialize<TEvent>(args.Body.ToArray()));
        }
        catch (Exception exception)
        {
          channel.BasicNack(args.DeliveryTag, false, true);
        }

        channel.BasicAck(args.DeliveryTag, false);
      };

      var consumerTag = channel.BasicConsume(queueName, false, consumer);

      this._subscriptions.Add(Subscription.CreateNew<TEvent, THandler>(consumerTag));
    }

    public void Unsubscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
    {
      foreach (var subscriptionToRemove in this._subscriptions.Where(s => s.Check<T, TH>()))
      {
        this.Unsubscribe(subscriptionToRemove);
      }
    }

    public void Dispose()
    {
      foreach (var subscription in this._subscriptions)
      {
        this.Unsubscribe(subscription);
      }

      _consumeChannel?.Dispose();
      _connection?.Dispose();
    }
  }
}
