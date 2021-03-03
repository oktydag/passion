using System.Threading.Tasks;

namespace Passion.Rover.Command.Services
{
    public interface IRoverService
    {
        void SendPassionToMars();
        Task<Domain.Aggregates.Rover> GetCurrentRover();
    }
}