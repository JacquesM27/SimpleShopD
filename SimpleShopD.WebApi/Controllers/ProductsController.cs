using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleShopD.Application.Commands.Products.Add;
using SimpleShopD.Application.Commands.Products.Delete;
using SimpleShopD.Application.Commands.Products.Update;
using SimpleShopD.Application.Queries.Products.Get;
using SimpleShopD.Application.Queries.Products.GetAll;
using SimpleShopD.Domain.Users.ValueObjects;
using SimpleShopD.Shared.Abstractions.Commands;
using SimpleShopD.Shared.Abstractions.Queries;

namespace SimpleShopD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly ICommandTResultDispatcher _commandTResultDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public ProductsController(ICommandDispatcher commandDispatcher,
            ICommandTResultDispatcher commandTResultDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _commandTResultDispatcher = commandTResultDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        public async Task<IActionResult> Get(GetProduct command)
        {
            var result = await _queryDispatcher.QueryAsync(command);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllProducts(GetAllProducts command)
        {
            var result = await _queryDispatcher.QueryAsync(command);
            return Ok(result);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("add")]
        public async Task<IActionResult> Add(AddProduct command)
        {
            var result = await _commandTResultDispatcher.DispatchAsync(command);
            return Ok(result);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(DeleteProduct command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Ok();
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPatch("update")]
        public async Task<IActionResult> Update(UpdateProduct command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Ok();
        }
    }
}