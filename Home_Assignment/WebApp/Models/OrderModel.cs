namespace WebApp.Models
{
    public class OrderModel
    {
        public string? OrderId { get; set; }
        public string? VideoId { get; set; }
        public string? UserId { get; set; }
        public decimal? Price { get; set; }
        public DateTime? OrderDate { get; set; }
    }
}
