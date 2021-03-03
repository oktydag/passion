using MediatR;

namespace Passion.Rover.Command.Commands
{
    public class GoGivenLocationCommand : IRequest<bool>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Direction { get; set; }
    }
}