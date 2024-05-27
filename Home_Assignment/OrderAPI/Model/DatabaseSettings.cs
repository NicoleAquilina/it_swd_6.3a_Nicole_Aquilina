namespace OrderAPI.Model
{
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string OrderCollectionName { get; set; } = null!;
    }
}
