using System;

 namespace Passion.Rover.Query.Consumer.Events.V1
{
    public class LocationChanged : IEvent
    {
        public string EventId { get;  set; }
        public string Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Direction { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}