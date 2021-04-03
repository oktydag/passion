using System;
using System.Threading.Tasks;
using MassTransit;
using Passion.Rover.Process.Consumer.Consumers;
using Passion.Rover.Query.Consumer.Services.Contracts;

namespace Passion.Rover.Query.Consumer.Services
{
    public class BusService : IBusService
    {
        private readonly IElasticsearchService _elasticsearchService;

        public BusService(IElasticsearchService elasticsearchService)
        {
            _elasticsearchService = elasticsearchService;
        }

        IBusControl _bus;

        public bool Start()
        {
            _bus = ConfigureBus();
            _bus.Start(TimeSpan.FromSeconds(1000));

            return true;
        }

        public async Task Publish<TEvent>(TEvent @event)
        {
            var sendToUri =
                new Uri(
                    $"{ConnectionConstants.HostAddress}{ConnectionConstants.ProviderPassionCommandStatusQueueName}");

            var sendEndpoint = _bus.GetSendEndpoint(sendToUri).Result;
            await sendEndpoint.Send(@event);
        }


        public bool Stop()
        {
            _bus?.Stop(TimeSpan.FromSeconds(30));

            return true;
        }

        private IBusControl ConfigureBus()
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri(ConnectionConstants.HostAddress), k =>
                {
                    k.Username(ConnectionConstants.Username);
                    k.Password(ConnectionConstants.Password);
                });
                cfg.ReceiveEndpoint(ConnectionConstants.SubscribeQueueName, e =>
                {
                    e.Consumer(() => new PhotoWasTakenConsumer(this, _elasticsearchService));
                    e.Consumer(() => new LocationChangedConsumer(this, _elasticsearchService));
                    e.Consumer(() => new SampleCollectedConsumer(this, _elasticsearchService));
                });
            });
        }
    }


    public class ConnectionConstants
    {
        public static string HostAddress = "rabbitmq://localhost/";
        public static string Username = "od";
        public static string Password = "od1234";
        public static string SubscribeQueueName = "Out.Passion.Command";
        public static string ProviderPassionCommandStatusQueueName = "In.Passion.Command.Status";
    }
}