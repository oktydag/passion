using System.Threading.Tasks;

namespace Passion.Rover.Process.Consumer.Services.Contracts
{
    public interface IBusService
    {
        bool Start();
        bool Stop();
        Task Publish<TEvent>(TEvent @event);
    }
}