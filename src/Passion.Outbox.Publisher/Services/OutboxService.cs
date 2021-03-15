using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Passion.Outbox.Publisher.Documents;
using Passion.Outbox.Publisher.Services.Contracts;
using Passion.Outbox.Publisher.Settings;

namespace Passion.Outbox.Publisher.Services
{
    public class OutboxService : IOutboxService
    {
        private const string READY = "Ready";
        private const string INPROGRESS = "InProgress";
        private const string PENDING = "Pending";
        private const string FAILED = "Failed";
        // No need to update "Status" as *Done* due to message will be deleted for success process scnerio

        private readonly IMongoCollection<OutboxMessage> _mongoCollection;
        
        public OutboxService(IOptions<DatabaseSettings> databaseSettings)
        {
            // _mongoCollection = mongoCollection;
            var client = new MongoClient(databaseSettings.Value.ConnectionString);
            var database = client.GetDatabase(databaseSettings.Value.DatabaseName);

            _mongoCollection = database.GetCollection<OutboxMessage>(databaseSettings.Value.CollectionName);
        }

        public async Task<OutboxMessage> Process()
        {
            return await _mongoCollection.FindOneAndUpdateAsync(
                Builders<OutboxMessage>.Filter.Eq(x => x.Status, READY),
                Builders<OutboxMessage>.Update.Set(x => x.Status, INPROGRESS),
                new FindOneAndUpdateOptions<OutboxMessage>()
                {
                    Sort = Builders<OutboxMessage>.Sort.Ascending(x => x.OccurredOn)
                }
            );
        }

        public async Task<OutboxMessage> DeleteMessageById(ObjectId id)
        {
            return await _mongoCollection.FindOneAndDeleteAsync(
                Builders<OutboxMessage>.Filter.Eq(nameof(id), false));
        }

        public async Task<OutboxMessage> MarkAsFailed(ObjectId id)
        {
            return await Mark(id, FAILED);
        }
        
        public async Task<OutboxMessage> MarkAsPending(ObjectId id)
        {
            return await Mark(id, PENDING);
        }
        private async Task<OutboxMessage> Mark(ObjectId id, string status)
        {
          return await _mongoCollection.FindOneAndUpdateAsync(
                Builders<OutboxMessage>.Filter.Eq(x => x.Id, id),
                Builders<OutboxMessage>.Update.Set(x => x.Status, status)
                    .Set(x => x.OccurredOn, DateTime.Now.ToUniversalTime()));
        }
    }
}