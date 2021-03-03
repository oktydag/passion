namespace Passion.Rover.Command.Settings
{
    public class DatabaseSettings: IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string OutboxCollectionName { get; set; }
        
        public string RoverCollectionName { get; set; }
    }
}