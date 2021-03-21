using Passion.Rover.Process.Consumer.Events;

namespace Passion.Events.V1
{
    public class PhotoWasTaken : IEvent
    {
        public string Id { get;  set; }
        public string Name { get; set; }
        public double Size { get; set; }
        public string ImageAsFormatted { get; set; }
    }
}