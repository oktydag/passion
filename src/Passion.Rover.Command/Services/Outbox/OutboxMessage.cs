using System;
using Passion.Rover.Command.Domain.SeedWork;

namespace Passion.Rover.Command.Services.Outbox
{
    public class OutboxMessage : Entity
    {
        public OutboxMessage(string type, string data, DateTime occurredOn)
        {
            Type = type;
            Data = data;
            Status = "Ready";
            OccurredOn = DateTime.Now.ToUniversalTime();
        }

        public string Type { get; set; }
        public string Data { get; set; }
        public string Status { get; protected set; }
        public DateTime OccurredOn { get; set; }
 

    }
}