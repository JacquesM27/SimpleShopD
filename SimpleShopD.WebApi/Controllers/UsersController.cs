using Microsoft.AspNetCore.Mvc;
using SimpleShopD.Application.Commands.Users.Login;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly ICommandTResultDispatcher _commandTResultDispatcher;

        public UsersController(ICommandDispatcher commandDispatcher, ICommandTResultDispatcher commandTResultDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _commandTResultDispatcher = commandTResultDispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUser loginUser)
        {
            var result = await _commandTResultDispatcher.DispatchAsync(loginUser);
            return Ok(result);
        }
    }
}
