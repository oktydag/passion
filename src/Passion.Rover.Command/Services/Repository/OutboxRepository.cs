using System.Threading.Tasks;
using MongoDB.Driver;
using Passion.Rover.Command.Services.Outbox;
using Passion.Rover.Command.Settings;

namespace Passion.Rover.Command.Services.Repository
{
    public class OutboxRepository : IOutboxRepository
    {
        private readonly IMongoCollection<OutboxMessage> _outboxMessage;

        public OutboxRepository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _outboxMessage = database.GetCollection<OutboxMessage>(settings.OutboxCollectionName);
        }
        public async Task InsertAsync(OutboxMessage outboxMessage)
        {
           await _outboxMessage.InsertOneAsync(outboxMessage);
        }
    }
}