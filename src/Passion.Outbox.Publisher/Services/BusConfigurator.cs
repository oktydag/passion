using System;
using MassTransit;
using MassTransit.RabbitMqTransport;

namespace Passion.Outbox.Publisher.Services
{
    public class BusConfigurator
    {
        private static readonly Lazy<BusConfigurator> _Instance = new Lazy<BusConfigurator>(() => new BusConfigurator());

        private BusConfigurator()
        {

        }

        public static BusConfigurator Instance => _Instance.Value;

        public IBusControl ConfigureBus(
            Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost> registrationAction = null)
        {
            
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(ConnectionConstants.HostAddress), hst =>
                {
                    hst.Username(ConnectionConstants.Username);
                    hst.Password(ConnectionConstants.Password);
                });

                registrationAction?.Invoke(cfg, host);
            });
        }
    }

    public class ConnectionConstants
    {
        public static string HostAddress = "rabbitmq://rabbitmq/";
        public static string Username = "od";
        public static string Password = "od1234";
        public static string PublishmentQueueName = "Out.Passion.Command";
        public static string ConsumeQueueName = "In.Passion.Command.Status";
    }
}