using System.ComponentModel.Design;
using MediatR;

namespace Passion.Rover.Command.Commands
{
    public class CollectSampleCommand : IRequest<bool>
    {
        public string ObjectName { get; set; }
        public double ObjectAmount { get; set; }
    }
}