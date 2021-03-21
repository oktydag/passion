﻿using System;
using Passion.Outbox.Publisher.Events;

namespace Passion.Events.V1
{
    public class LocationChanged : IEvent
    {
        public string Id { get;  set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Direction { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}