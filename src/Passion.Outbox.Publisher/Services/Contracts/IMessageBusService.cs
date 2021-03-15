using System.Threading.Tasks;

namespace Passion.Outbox.Publisher.Services.Contracts
{
    public interface IMessageBusService
    {
        Task Publish<TEvent>(TEvent @event);
        Task Consume<TEvent>(TEvent @event);
    }
}