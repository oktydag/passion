namespace Passion.Rover.Command.Settings
{
    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string OutboxCollectionName { get; set; }
        string RoverCollectionName { get; set; }
    }
}