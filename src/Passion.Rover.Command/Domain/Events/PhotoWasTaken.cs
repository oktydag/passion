namespace Passion.Rover.Command.Domain.Events
{
    public class PhotoWasTaken : IEvent
    {
        public string Id { get;  set; }
        public string Name { get; set; }
        public double Size { get; set; }
        public string ImageAsFormatted { get; set; }
        
        public string GetEventName()
        {
            return this.GetType().FullName;
        }
    }
}