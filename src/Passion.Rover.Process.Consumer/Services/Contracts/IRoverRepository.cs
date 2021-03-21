using System.Threading.Tasks;
using Passion.Events.V1;
using Passion.Rover.Process.Consumer.Entities;

namespace Passion.Rover.Process.Consumer.Services.Contracts
{
    public interface IRoverService
    {
        Task<bool> InsertAsync(Entities.Rover rover);
        Task SavePhoto(Photo photo);
        Task SetNewLocation(Location location);
        Task SaveSample(Sample sample);
    }
}