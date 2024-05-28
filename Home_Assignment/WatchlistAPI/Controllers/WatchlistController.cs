using Microsoft.AspNetCore.Mvc;
using VideoCatalogueAPI.Model;
using WatchlistAPI.Model;
using WatchlistAPI.Services;

namespace WatchlistAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchlistController : ControllerBase
    {
        private readonly WatchlistService _context;

        public WatchlistController(WatchlistService context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] Watchlist w)
        {
            var u = await _context.GetAllAsync(w.UserId);

            if (u != null)
            {
                return BadRequest("User already has a Watch List");
            }

            var watchlist = new Watchlist();
            watchlist.UserId = w.UserId;
            watchlist.VideoIds = w.VideoIds;
            await _context.CreateAsync(watchlist);

            return Ok();
        }

        [HttpGet("getTitles")]
        public async Task<ActionResult<Watchlist>> GetTitles([FromQuery(Name = "userId")] string userId)
        {
            var u = await _context.GetAllAsync(userId);

            if (u == null)
            {
                return BadRequest("This User does not have a WatchList");
            }
            return Ok(u);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> RemoveVideo([FromQuery(Name = "userId")] string userId, [FromQuery(Name = "videoId")] string videoId)
        {
            await _context.RemoveAsync(userId, videoId);

            return Ok();
        }

    }
}
