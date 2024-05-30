namespace WebApp.Models
{
    public class PaymentModel
    {
        public string? Id { get; set; }
        public string? OrderId { get; set; }
        public string? UserId { get; set; }
        public decimal? Price { get; set; }
        public DateTime? DatePaid { get; set; }
    }
}
