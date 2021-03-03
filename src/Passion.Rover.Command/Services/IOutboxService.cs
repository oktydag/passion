using System.Threading.Tasks;
using Passion.Rover.Command.Services.Outbox;

namespace Passion.Rover.Command.Services
{
    public interface IOutboxService
    {
        Task<bool> CreateOutboxMessage(OutboxMessage message);
    }
}