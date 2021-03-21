namespace Passion.Rover.Process.Consumer
{
    public class DatabaseSettings:  IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}