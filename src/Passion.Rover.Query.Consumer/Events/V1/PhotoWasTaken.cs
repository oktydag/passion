using Passion.Rover.Query.Consumer.Events;

namespace Passion.Rover.Query.Consumer.Events.V1
{
    public class PhotoWasTaken : IEvent
    {
        public string EventId { get;  set; }
        public string Id { get;  set; }
        public string Name { get; set; }
        public double Size { get; set; }
        public string ImageAsFormatted { get; set; }
    }
}