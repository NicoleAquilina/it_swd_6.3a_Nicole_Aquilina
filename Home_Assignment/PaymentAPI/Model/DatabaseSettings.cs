namespace PaymentAPI.Model
{
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string PaymentCollectionName { get; set; } = null!;
    }
}
