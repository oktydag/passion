using System.Threading.Tasks;
using MassTransit;
using MongoDB.Bson;
using Passion.Events.V1;
using Passion.Rover.Process.Consumer.Entities;
using Passion.Rover.Process.Consumer.Services.Contracts;

namespace Passion.Rover.Process.Consumer.Consumers
{
    public class PhotoWasTakenConsumer : IConsumer<PhotoWasTaken>
    {
        private readonly IRoverService _roverService;
        private readonly IBusService _busService;

        public PhotoWasTakenConsumer(IRoverService roverService, IBusService busService)
        {
            _roverService = roverService;
            _busService = busService;
        }

        public async Task Consume(ConsumeContext<PhotoWasTaken> context)
        {
            var message = context.Message;

            await _roverService.SavePhoto(new Photo()
            {
                Id = ObjectId.Parse(message.Id),
                Name = message.Name,
                Size = message.Size,
                ImageAsFormatted = message.ImageAsFormatted
            }).ConfigureAwait(true);
            
            await _busService.Publish(new MessageProcessed()
            {
                EventId = message.Id,
                IsReceivedSuccessfully = true
            });
            
        }
    }
}