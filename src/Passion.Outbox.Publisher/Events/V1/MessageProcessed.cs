﻿namespace Passion.Events.V1
{
    public class MessageProcessed
    {
        public string Id { get;  set; }
        public bool IsReceivedSuccessfully { get; set; }
    }
}