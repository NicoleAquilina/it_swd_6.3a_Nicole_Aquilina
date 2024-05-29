using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VideoCatalogueAPI.Model;
using Microsoft.AspNetCore.Authorization;

namespace VideoCatalogueAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {

        [HttpGet("genre")]
        [Authorize]
        public async Task<ActionResult<List<Video>>> GetVideosByGenre(string genre)
        {
            var client = new RestClient("https://moviesdatabase.p.rapidapi.com");
            var request = new RestRequest($"/titles?genre={genre}");
            request.AddHeader("x-rapidapi-key", "f95eee0b14msh8cdb9371aab938ep1d1949jsn8c180758d997");
            request.AddHeader("x-rapidapi-host", "moviesdatabase.p.rapidapi.com");

            List<Video> videos = new List<Video>();
            var response = await client.GetAsync(request);
            dynamic content = JsonConvert.DeserializeObject<JToken>(response.Content);
            foreach(var result in content.results)
            {
                string imageUrl = null;

                if(result.primaryImage !=  null && result.primaryImage.url !=null)
                {
                    imageUrl = result.primaryImage.url;
                }
                var video = new Video()
                {
                    Id = result.id,
                    Title = result.titleText.text,
                    ReleaseYear = result.releaseYear.year,
                    PictureURI = imageUrl
                };

                videos.Add(video);
            }
            return Ok(videos);
        }

        [HttpGet("title")]
        [Authorize]
        public async Task<ActionResult<Video>> GetTitle(string Id)
        {
            var client = new RestClient("https://moviesdatabase.p.rapidapi.com");
            var request = new RestRequest($"/titles/{Id}");
            request.AddHeader("x-rapidapi-key", "f95eee0b14msh8cdb9371aab938ep1d1949jsn8c180758d997");
            request.AddHeader("x-rapidapi-host", "moviesdatabase.p.rapidapi.com");

            var response = await client.GetAsync(request);
            dynamic result = JsonConvert.DeserializeObject<JToken>(response.Content);
            result = result.results;
            string imageUrl = null;

            if (result.primaryImage != null && result.primaryImage.url != null)
            {
                imageUrl = result.primaryImage.url;
            }
            var video = new Video()
            {
                Id = result.id,
                Title = result.titleText.text,
                ReleaseYear = result.releaseYear.year,
                PictureURI = imageUrl
            };
            return Ok(video);
        }
    }
}
