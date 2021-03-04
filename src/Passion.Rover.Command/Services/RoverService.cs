using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using Passion.Rover.Command.Domain.Aggregates;
using Passion.Rover.Command.Domain.Types;
using Passion.Rover.Command.Services.Repository;

namespace Passion.Rover.Command.Services
{
    public class RoverService: IRoverService
    {
        private readonly IRoverRepository _roverRepository;

        public RoverService(IRoverRepository roverRepository)
        {
            _roverRepository = roverRepository;
        }

        public async Task<Domain.Aggregates.Rover> GetCurrentRover()
        {
            var rover = _roverRepository.GetById(Rovers.Passion);
            return rover.Result;
        }
        public void SendPassionToMars()
        {
            var passionRover = _roverRepository.GetById(Rovers.Passion);
            
            if (passionRover.Result == null)
            {
                var newPassionRover = new Domain.Aggregates.Rover(ObjectId.Parse(Rovers.Passion),"Passion", DateTime.Now, new CameraEngine(),
                    new MovementEngine(new Location(0,0,Direction.North), DateTime.Now.ToUniversalTime() ), new SampleCollectorEngine());
                
              _roverRepository.InsertAsync(newPassionRover);
            }
        }
    }
}