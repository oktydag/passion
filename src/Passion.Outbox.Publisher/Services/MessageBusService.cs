using System;
using System.Threading.Tasks;
using MassTransit;
using Passion.Outbox.Publisher.Services.Contracts;

namespace Passion.Outbox.Publisher.Services
{
    public class MessageBusService : IMessageBusService
    {
        private readonly ISendEndpoint _sendEndpoint;

        public MessageBusService()
        {
            var busControl = BusConfigurator.Instance.ConfigureBus();
            var sendToUri = new Uri($"{ConnectionConstants.HostAddress}{ConnectionConstants.PublishmentQueueName}");

            _sendEndpoint = busControl.GetSendEndpoint(sendToUri).Result;
        }

        public async Task Publish<TEvent>(TEvent @event)
        {
            await _sendEndpoint.Send(@event);
        }

        public async Task Consume<TEvent>(TEvent @event)
        {
        }
    }
}