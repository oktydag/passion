namespace Passion.Rover.Command.Domain.Events
{
    public class SampleCollected : IEvent
    {
        public string Id { get; set; }
        public string ObjectName { get;  set; }
        public double ObjectAmountAsGram { get; set; }
        
        public string GetEventName()
        {
            return this.GetType().FullName;
        }
    }
}