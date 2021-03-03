using System.Threading.Tasks;
using Passion.Rover.Command.Services.Outbox;
using Passion.Rover.Command.Services.Repository;

namespace Passion.Rover.Command.Services
{
    public class OutboxService : IOutboxService
    {
        private readonly IOutboxRepository _outboxRepository;

        public OutboxService(IOutboxRepository outboxRepository)
        {
            _outboxRepository = outboxRepository;
        }

        public async Task<bool> CreateOutboxMessage(OutboxMessage message)
        {
            await _outboxRepository.InsertAsync(message);
            return true;
        }
    }
}