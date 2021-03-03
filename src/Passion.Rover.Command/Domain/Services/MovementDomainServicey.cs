using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Passion.Rover.Command.Domain.Aggregates;
using Passion.Rover.Command.Domain.Events;
using Passion.Rover.Command.Domain.Services.Contracts;
using Passion.Rover.Command.Services;
using Passion.Rover.Command.Services.Outbox;

namespace Passion.Rover.Command.Domain.Services
{
    public class MovementDomainService : IMovementDomainService
    {
        private IOutboxService _outboxService;
        private IRoverService _roverService;

        public MovementDomainService(IOutboxService outboxService, IRoverService roverService)
        {
            _outboxService = outboxService;
            _roverService = roverService;
        }

        public async Task<bool> Go(int xCoordinate, int yCoordinate, string direction)
        {
            var passion = _roverService.GetCurrentRover();

            var directionOfPassionRoverKnow = (Direction)char.Parse(direction);

            var engineWithNewLocation =
                passion.Result.MovementEngine.Go(new Location(xCoordinate, yCoordinate, directionOfPassionRoverKnow));

            var data = new LocationChanged()
            {
                Id = engineWithNewLocation.Id.ToString(),
                X = engineWithNewLocation.Location.X,
                Y = engineWithNewLocation.Location.Y,
                Direction = direction,
                UpdatedDate = engineWithNewLocation.UpdatedDate
            };

            return await _outboxService.CreateOutboxMessage(new OutboxMessage(type: data.GetEventName(),
                JsonConvert.SerializeObject(data), DateTime.Now));
        }
    }
}