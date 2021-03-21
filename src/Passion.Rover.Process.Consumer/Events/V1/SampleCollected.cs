using Passion.Rover.Process.Consumer.Events;

namespace Passion.Events.V1
{
    public class SampleCollected : IEvent
    {
        public string Id { get; set; }
        public string ObjectName { get;  set; }
        public double ObjectAmountAsGram { get; set; }
    }
}