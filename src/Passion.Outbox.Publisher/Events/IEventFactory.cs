using System;

namespace Passion.Outbox.Publisher.Events
{
    public interface IEventFactory
    {
        Type FindEventType(string eventName);
    }
}