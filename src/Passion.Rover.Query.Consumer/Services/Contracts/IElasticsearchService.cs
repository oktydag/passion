using System.Threading.Tasks;

namespace Passion.Rover.Query.Consumer.Services.Contracts
{
    public interface IElasticsearchService
    {
        Task Write<TEvent>(TEvent @event);
    }
}