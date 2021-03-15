using System.Threading.Tasks;
using MongoDB.Bson;
using Passion.Outbox.Publisher.Documents;

namespace Passion.Outbox.Publisher.Services.Contracts
{
    public interface IOutboxService
    {
        Task<OutboxMessage> Process();
        Task<OutboxMessage> DeleteMessageById(ObjectId id);
        Task<OutboxMessage> MarkAsFailed(ObjectId id);
        Task<OutboxMessage> MarkAsPending(ObjectId id);
    }
}