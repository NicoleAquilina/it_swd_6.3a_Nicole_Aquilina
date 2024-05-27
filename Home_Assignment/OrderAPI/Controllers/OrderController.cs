using Microsoft.AspNetCore.Mvc;
using OrderAPI.Model;
using OrderAPI.Services;
using System.Numerics;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _context;

        public OrderController(OrderService context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] Order o)
        {
            var u = await _context.GetByOrderIdAsync(o.OrderId);

            if (u != null)
            {
                return BadRequest("OrderID already exists");
            }

            var order = new Order();
            order.VideoId= o.VideoId;
            order.UserId= o.UserId;
            order.Price= o.Price;
            order.OrderDate = DateTime.Now;

            await _context.CreateAsync(order);

            return Ok();
        }

        [HttpGet("orders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders([FromQuery(Name = "userId")] string userId)
        {
            var u = await _context.GetAllAsync(userId);

            if (u == null)
            {
                return BadRequest("No orders were allocated to this user");
            }
            return Ok(u);
        }

        [HttpGet("orderDetails")]
        public async Task<ActionResult<Order>> GetOrderbyID([FromQuery(Name = "orderId")] string orderId)
        {
            var u = await _context.GetByOrderIdAsync(orderId);

            if (u == null)
            {
                return BadRequest("No orders were allocated to this user");
            }
            return Ok(u);
        }
    }
}
