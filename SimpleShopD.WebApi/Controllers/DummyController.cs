using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace SimpleShopD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DummyController : ControllerBase
    {
        [HttpPost]
        public Task<IActionResult> Hi()
        {
            return Task.FromResult<IActionResult>(Ok(JsonSerializer.Serialize(new { message = "hello"})));
        }

        [Authorize]
        [HttpPost("/auth")]
        public Task<IActionResult> AuthorizedHi()
        {
            return Task.FromResult<IActionResult>(Ok(JsonSerializer.Serialize(new { message = "authorized hello" })));
        }
    }
}
