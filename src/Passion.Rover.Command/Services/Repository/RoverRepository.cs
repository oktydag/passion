using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Passion.Rover.Command.Domain.Aggregates;
using Passion.Rover.Command.Settings;

namespace Passion.Rover.Command.Services.Repository
{
    public class RoverRepository : IRoverRepository
    {
        private readonly IMongoCollection<Domain.Aggregates.Rover> _rover;

        public RoverRepository(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _rover = database.GetCollection<Domain.Aggregates.Rover>(databaseSettings.RoverCollectionName);
        }
        
        public async Task<bool> InsertAsync(Domain.Aggregates.Rover rover)
        {
            await _rover.InsertOneAsync(rover);
            return true;
        }
        
        public async Task<Domain.Aggregates.Rover> GetById(string id)
        {
            var rover = _rover.FindSync(x => x.Id == ObjectId.Parse(id));
                return rover.FirstOrDefault();
        }
    }
}