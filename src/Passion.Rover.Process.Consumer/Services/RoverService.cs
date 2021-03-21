using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Passion.Events.V1;
using Passion.Rover.Process.Consumer.Entities;
using Passion.Rover.Process.Consumer.Services.Contracts;

namespace Passion.Rover.Process.Consumer.Services
{
    public class RoverService : IRoverService
    {
        private readonly IMongoCollection<Entities.Rover> _rover;
        private static ObjectId CURRENT_ROVER_ID = ObjectId.Parse("603e9272e16a84e9e5405895");

        public RoverService(IOptions<DatabaseSettings> databaseSettings)
        {
            var client = new MongoClient(databaseSettings.Value.ConnectionString);
            var database = client.GetDatabase(databaseSettings.Value.DatabaseName);

            _rover = database.GetCollection<Entities.Rover>(databaseSettings.Value.CollectionName);
        }

        public async Task<bool> InsertAsync(Entities.Rover rover)
        {
            await _rover.InsertOneAsync(rover);
            return true;
        }

        private Entities.Rover GetById(ObjectId id)
        {
            var rover = _rover.FindSync(x => x.Id == id).FirstOrDefault();
            return rover;
        }

        public async Task SavePhoto(Photo photo)
        {
            var currentRover = GetById(CURRENT_ROVER_ID);

            var photos = currentRover.CameraEngine.Photos;
            if (photos == null)
            {
                photos = new List<Photo>();
            }

            photos.Add(photo);

            var builder = Builders<Entities.Rover>.Update;
            var update = builder.Set("CameraEngine.Photos", photos);

            var filter = Builders<Entities.Rover>.Filter.Eq("_id", CURRENT_ROVER_ID);

            await _rover.UpdateOneAsync(filter, update);
        }
        
        public async Task SetNewLocation(Location location)
        {
            var builder = Builders<Entities.Rover>.Update;
            var update = builder.Set("MovementEngine.Location", location);

            var filter = Builders<Entities.Rover>.Filter.Eq("_id", CURRENT_ROVER_ID);

            await _rover.UpdateOneAsync(filter, update);
        }
        
        public async Task SaveSample(Sample sample)
        {
            var currentRover = GetById(CURRENT_ROVER_ID);

            var samples = currentRover.SampleCollectorEngine.Samples;
            if (samples == null)
            {
                samples = new List<Sample>();
            }
            samples.Add(sample);
            
            var builder = Builders<Entities.Rover>.Update;
            var update = builder.Set("SampleCollectorEngine.Samples", samples);

            var filter = Builders<Entities.Rover>.Filter.Eq("_id", CURRENT_ROVER_ID);

            await _rover.UpdateOneAsync(filter, update);
        }
    }
}