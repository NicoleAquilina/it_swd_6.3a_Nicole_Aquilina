using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<ActionResult> Create([FromBody] Watchlist w)
        {
            var u = await _context.GetAllAsync(w.UserId);

            if (u != null)
            {
                //check if that video already exists
                var check = await _context.CheckVideoExistsInWatchlistAsync(w.UserId, w.VideoIds);
                if(check == false)
                {
                    //append
                    await _context.UpdateAsync(w.UserId, w.VideoIds);
                    return Ok();
                }else
                {
                    return BadRequest("This Video Already exists in Watchlist");

                }

            }
            //create
            var watchlist = new Watchlist();
            watchlist.UserId = w.UserId;
            watchlist.VideoIds = w.VideoIds;
            await _context.CreateAsync(watchlist);

            return Ok();
        }

        [HttpGet("get")]
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> RemoveVideo([FromQuery(Name = "userId")] string userId, [FromQuery(Name = "videoId")] string videoId)
        {
            await _context.RemoveAsync(userId, videoId);

            return Ok();
        }
    }
}
