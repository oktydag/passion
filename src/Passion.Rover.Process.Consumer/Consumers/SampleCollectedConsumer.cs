using System.Threading.Tasks;
using MassTransit;
using MongoDB.Bson;
using Passion.Events.V1;
using Passion.Rover.Process.Consumer.Entities;
using Passion.Rover.Process.Consumer.Services.Contracts;

namespace Passion.Rover.Process.Consumer.Consumers
{
    public class SampleCollectedConsumer : IConsumer<SampleCollected>
    {
        private readonly IRoverService _roverService;
        private readonly IBusService _busService;

        public SampleCollectedConsumer(IRoverService roverService, IBusService busService)
        {
            _roverService = roverService;
            _busService = busService;
        }

        
        public async Task Consume(ConsumeContext<SampleCollected> context)
        {
            var message = context.Message;

            await _roverService.SaveSample(new Sample()
            {
                Id = ObjectId.Parse(message.Id),
                ObjectName = message.ObjectName,
                ObjectAmountAsGram = message.ObjectAmountAsGram
            });
            
            await _busService.Publish(new MessageProcessed()
            {
                EventId = message.Id,
                IsReceivedSuccessfully = true
            });
        }
    }
}