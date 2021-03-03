using System.ComponentModel.Design;
using MediatR;

namespace Passion.Rover.Command.Commands
{
    public class TakeWhatYouSeeCommand : IRequest<bool>
    {
        public string ObjectName { get; set; }
    }
}