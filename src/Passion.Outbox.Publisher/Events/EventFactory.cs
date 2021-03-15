using System;
using System.Collections.Generic;
using Passion.Outbox.Publisher.Events.V1;

namespace Passion.Outbox.Publisher.Events
{
    public class EventFactory : IEventFactory
    {
        private Dictionary<string, Type> eventMapping = new Dictionary<string, Type>()
        {
            {"Passion.Rover.Command.Domain.Events.PhotoWasTaken", typeof(PhotoWasTaken)},
            {"Passion.Rover.Command.Domain.Events.LocationChanged", typeof(LocationChanged)},
            {"Passion.Rover.Command.Domain.Events.SampleCollected", typeof(SampleCollected)}
        };

        public Type FindEventType(string eventName)
        {
            bool isSucceded = eventMapping.TryGetValue(eventName, out var selectedEvent);
            if (!isSucceded)
                throw new ArgumentOutOfRangeException("Invalid Event Name");

            return selectedEvent;
        }
    }
}