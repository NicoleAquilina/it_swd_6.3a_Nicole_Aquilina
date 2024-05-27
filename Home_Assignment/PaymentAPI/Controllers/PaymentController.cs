using Microsoft.AspNetCore.Mvc;
using PaymentAPI.Model;
using PaymentAPI.Services;

namespace PaymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _context;

        public PaymentController(PaymentService context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] Payment p)
        {
            var u = await _context.GetAsync(p.OrderId);

            if (u != null)
            {
                return BadRequest("Payment was already accepted.");
            }

            var payment = new Payment();
            payment.OrderId= p.OrderId;
            payment.Price= p.Price; 
            payment.DatePaid= DateTime.Now;

            await _context.CreateAsync(payment);

            return Ok();
        }

        [HttpGet("create")]
        public async Task<ActionResult<Payment>> Get([FromQuery(Name = "orderId")] string orderID)
        {
            var u = await _context.GetAsync(orderID);

            if (u == null)
            {
                return BadRequest("Payment has not been accepted");
            }
            return Ok(u);
        }

    }
}
