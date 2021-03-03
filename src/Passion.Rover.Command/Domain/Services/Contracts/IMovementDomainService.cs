using System.Threading.Tasks;

namespace Passion.Rover.Command.Domain.Services.Contracts
{
    public interface IMovementDomainService
    {
       Task<bool> Go(int xCoordinate, int yCoordinate, string direction);
    }
}