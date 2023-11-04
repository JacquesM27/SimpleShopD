using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleShopD.Application.Commands.Users.Activate;
using SimpleShopD.Application.Commands.Users.AddressAdd;
using SimpleShopD.Application.Commands.Users.AddressRemove;
using SimpleShopD.Application.Commands.Users.ChangeUserPassword;
using SimpleShopD.Application.Commands.Users.Login;
using SimpleShopD.Application.Commands.Users.NewPassword;
using SimpleShopD.Application.Commands.Users.RefreshToken;
using SimpleShopD.Application.Commands.Users.ResetPasswordToken;
using SimpleShopD.Application.Commands.Users.RoleChange;
using SimpleShopD.Application.Commands.Users.UserRegister;
using SimpleShopD.Domain.Users.ValueObjects;
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

        [HttpPatch("activate")]
        public async Task<IActionResult> ActivateAccount(ActivateAccount command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Ok();
        }

        [Authorize]
        [HttpPost("address/add")]
        public async Task<IActionResult> AddressAdd(AddAddress command)
        {
            var result = await _commandTResultDispatcher.DispatchAsync(command);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("address/remove")]
        public async Task<IActionResult> AddressRemove(RemoveAddress command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Ok();
        }

        [Authorize]
        [HttpPatch("passowrd/change")]
        public async Task<IActionResult> ChangeUserPassword(ChangePassword command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUser command)
        {
            var result = await _commandTResultDispatcher.DispatchAsync(command);
            return Ok(result);
        }

        [HttpPatch("password/new")]
        public async Task<IActionResult> NewPassword(SetNewPassword command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Ok();
        }

        [HttpPatch("token/refresh")]
        public async Task<IActionResult> RefreshToken(GenerateRefreshToken command)
        {
            var result = await _commandTResultDispatcher.DispatchAsync(command);
            return Ok(result);
        }

        [HttpPatch("password/token/reset")]
        public async Task<IActionResult> ResetPasswordToken(GeneratePasswordResetToken command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Ok();
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPatch("role/change")]
        public async Task<IActionResult> RoleChange(ChangeRole command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> UserRegister(RegisterUser command)
        {
            var result = await _commandTResultDispatcher.DispatchAsync(command);
            return Ok(result);
        }
    }
}
