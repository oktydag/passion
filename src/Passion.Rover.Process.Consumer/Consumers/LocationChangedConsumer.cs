using System;
using System.Threading.Tasks;
using MassTransit;
using MongoDB.Bson;
using Passion.Events.V1;
using Passion.Rover.Process.Consumer.Entities;
using Passion.Rover.Process.Consumer.Services.Contracts;

namespace Passion.Rover.Process.Consumer.Consumers
{
    public class LocationChangedConsumer : IConsumer<LocationChanged>
    {
        private readonly IRoverService _roverService;
        private readonly IBusService _busService;

        public LocationChangedConsumer(IRoverService roverService, IBusService busService)
        {
            _roverService = roverService;
            _busService = busService;
        }

        public async Task Consume(ConsumeContext<LocationChanged> context)
        {
            var message = context.Message;

            await _roverService.SetNewLocation(new Location()
            {
                Id = ObjectId.Parse(message.Id),
                X = message.X,
                Y = message.Y,
                Direction = message.Direction
            });

            await _busService.Publish(new MessageProcessed()
            {
                EventId = message.Id,
                IsReceivedSuccessfully = true
            });

        }
    }
}