using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleShopD.Application.Commands.Products.Add;
using SimpleShopD.Application.Commands.Products.Delete;
using SimpleShopD.Application.Commands.Products.Update;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly ICommandTResultDispatcher _commandTResultDispatcher;

        public ProductsController(ICommandDispatcher commandDispatcher, ICommandTResultDispatcher commandTResultDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _commandTResultDispatcher = commandTResultDispatcher;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(AddProduct command)
        {
            var result = await _commandTResultDispatcher.DispatchAsync(command);
            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(DeleteProduct command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Ok();
        }

        [HttpPatch("update")]
        public async Task<IActionResult> Update(UpdateProduct command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Ok();
        }
    }
}
