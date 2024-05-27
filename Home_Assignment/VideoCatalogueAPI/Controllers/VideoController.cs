using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VideoCatalogueAPI.Model;

namespace VideoCatalogueAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {

        [HttpGet("genre")]
        public async Task<ActionResult<List<Video>>> GetVideosByGenre(string genre)
        {
            var client = new RestClient("https://moviesdatabase.p.rapidapi.com");
            var request = new RestRequest($"/titles?genre={genre}");
            request.AddHeader("x-rapidapi-key", "f95eee0b14msh8cdb9371aab938ep1d1949jsn8c180758d997");
            request.AddHeader("x-rapidapi-host", "moviesdatabase.p.rapidapi.com");

            List<Video> videos = new List<Video>();
            var response = await client.GetAsync(request);
            var content = JsonConvert.DeserializeObject<JToken>(response.Content);
            foreach(var result in content["results"])
            {
                var video = new Video();
                video.Id = result["id"].Value<string>();
                video.Title = result["titleText"]["text"].Value<string>();
                video.ReleaseYear = result["releaseYear"]["year"].Value<int>();

                videos.Add(video);
            }
            return Ok(videos);
        }

        [HttpGet("title")]
        public async Task<ActionResult<Video>> GetTitle(string Id)
        {
            var client = new RestClient("https://moviesdatabase.p.rapidapi.com");
            var request = new RestRequest($"/titles/{Id}");
            request.AddHeader("x-rapidapi-key", "f95eee0b14msh8cdb9371aab938ep1d1949jsn8c180758d997");
            request.AddHeader("x-rapidapi-host", "moviesdatabase.p.rapidapi.com");

            var response = await client.GetAsync(request);
            var content = JsonConvert.DeserializeObject<JToken>(response.Content);
            content = content["results"];
            var video = new Video();
            video.Id = content["id"].Value<string>();
            video.Title = content["titleText"]["text"].Value<string>();
            video.ReleaseYear = content["releaseYear"]["year"].Value<int>();

            return Ok(video);
        }
    }
}
