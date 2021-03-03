using System.Threading.Tasks;

namespace Passion.Rover.Command.Domain.Services.Contracts
{
    public interface ISampleCollectorDomainService
    {
       Task<bool> Collect(string objectName, double amountAsGram);
    }
}