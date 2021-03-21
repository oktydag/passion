using System.Threading.Tasks;
using MassTransit;
using MongoDB.Bson;
using Passion.Events.V1;
using Passion.Outbox.Publisher.Services.Contracts;

namespace Passion.Outbox.Publisher.Consumers
{
    public class MessageProcessedConsumer : IConsumer<MessageProcessed>
    {
        public MessageProcessedConsumer()
        {
            
        }

        private readonly IOutboxService _outboxService;

        public MessageProcessedConsumer(IOutboxService outboxService)
        {
            _outboxService = outboxService;
        }

        public async Task Consume(ConsumeContext<MessageProcessed> context)
        {
            var message = context.Message;

            if (message.IsReceivedSuccessfully)
            {
                await _outboxService.DeleteMessageById(ObjectId.Parse(message.Id));
            }
            else
            {
                await _outboxService.MarkAsFailed(ObjectId.Parse(message.Id));
            }
        }
    }
}