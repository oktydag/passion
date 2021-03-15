using System;

namespace Passion.Outbox.Publisher.Settings
{
    public class ProcessSettings : IProcessSettings
    {
        public string ExecutionLimit { get; set; } 
        public string ExecutionTryCount { get; set; }

        public int GetExecutionLimit()
        {
            return Convert.ToInt32(this.ExecutionLimit);
        }
    }
}