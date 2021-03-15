namespace Passion.Outbox.Publisher.Settings
{
    public interface IProcessSettings
    {
        string ExecutionLimit { get; set; }
        string ExecutionTryCount { get; set; }
        int GetExecutionLimit();
    }
}