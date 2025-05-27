using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChatWebSocketServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProxyController : ControllerBase
    {
        private static readonly HttpClient _httpClient = new();

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var response = await _httpClient.GetStringAsync("https://randomuser.me/api/?results=10");
            return Content(response, "application/json");
        }

        [HttpGet("images")]
        public async Task<IActionResult> GetImages()
        {
            var response = await _httpClient.GetStringAsync("https://picsum.photos/v2/list?page=2&limit=100");
            return Content(response, "application/json");
        }
    }
}
