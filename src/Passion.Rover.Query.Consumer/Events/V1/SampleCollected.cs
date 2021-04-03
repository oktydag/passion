namespace Passion.Rover.Query.Consumer.Events.V1
{
    public class SampleCollected : IEvent
    {
        public string EventId { get;  set; }
        public string Id { get; set; }
        public string ObjectName { get;  set; }
        public double ObjectAmountAsGram { get; set; }
    }
}