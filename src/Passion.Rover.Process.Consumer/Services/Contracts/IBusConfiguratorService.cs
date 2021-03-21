namespace Passion.Rover.Process.Consumer.Services.Contracts
{
    public interface IBusConfiguratorService
    {
        bool Start();
        bool Stop();
    }
}