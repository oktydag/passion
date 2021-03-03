using System.Threading.Tasks;

namespace Passion.Rover.Command.Domain.Services.Contracts
{
    public interface ICameraDomainService
    {
       Task<bool> TakeWhatYouSee(string objectName);
    }
}