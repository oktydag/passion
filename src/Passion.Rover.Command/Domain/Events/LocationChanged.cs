﻿using System;

namespace Passion.Rover.Command.Domain.Events
{
    public class LocationChanged : IEvent
    {
        public string Id { get;  set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Direction { get; set; }
        public DateTime UpdatedDate { get; set; }

        public string GetEventName()
        {
            return this.GetType().FullName;
        }
    }
}