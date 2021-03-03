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
    public class CameraDomainService : ICameraDomainService
    {
        private IOutboxService _outboxService;
        private IRoverService _roverService;

        public CameraDomainService(IOutboxService outboxService, IRoverService roverService)
        {
            _outboxService = outboxService;
            _roverService = roverService;
        }

        public async Task<bool> TakeWhatYouSee(string objectName)
        {
            var passion = _roverService.GetCurrentRover();

            var photosWithNewOne = passion.Result.CameraEngine.TakeNewPhoto(objectName);

            var data = new PhotoWasTaken()
            {
                Id = photosWithNewOne.Id.ToString(),
                Name = photosWithNewOne.Name,
                Size = photosWithNewOne.Size,
                ImageAsFormatted = photosWithNewOne.ImageAsFormatted
            };

           return await _outboxService.CreateOutboxMessage(new OutboxMessage(type: data.GetEventName(),
                JsonConvert.SerializeObject(data), DateTime.Now));
        }
    }
}