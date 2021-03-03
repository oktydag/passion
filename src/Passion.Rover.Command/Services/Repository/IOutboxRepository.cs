using System.Threading.Tasks;
using Passion.Rover.Command.Services.Outbox;

namespace Passion.Rover.Command.Services.Repository
{
    public interface IOutboxRepository
    {
        Task InsertAsync(OutboxMessage outboxMessage);
    }
}