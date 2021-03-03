using System.Threading.Tasks;

namespace Passion.Rover.Command.Services.Repository
{
    public interface IRoverRepository
    {
        Task<bool> InsertAsync(Domain.Aggregates.Rover rover);
       Task<Domain.Aggregates.Rover> GetById(string id);
    }
}