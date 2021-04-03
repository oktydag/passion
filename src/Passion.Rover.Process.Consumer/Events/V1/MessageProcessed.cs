namespace Passion.Events.V1
{
    public class MessageProcessed
    {
        public string EventId { get;  set; }
        public bool IsReceivedSuccessfully { get; set; }
    }
}