using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleShopD.Application.Commands.Orders.Add;
using SimpleShopD.Application.Commands.Orders.AddOrderLine;
using SimpleShopD.Application.Commands.Orders.Cancel;
using SimpleShopD.Application.Commands.Orders.ChangeDelivery;
using SimpleShopD.Application.Commands.Orders.Complete;
using SimpleShopD.Application.Commands.Orders.DeleteOrderLine;
using SimpleShopD.Application.Commands.Orders.Pay;
using SimpleShopD.Application.Commands.Orders.Pending;
using SimpleShopD.Application.Commands.Orders.Return;
using SimpleShopD.Application.Queries.Orders.Get;
using SimpleShopD.Application.Queries.Orders.GetBasic;
using SimpleShopD.Shared.Abstractions.Commands;
using SimpleShopD.Shared.Abstractions.Queries;

namespace SimpleShopD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly ICommandTResultDispatcher _commandTResultDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public OrdersController(ICommandDispatcher commandDispatcher, ICommandTResultDispatcher commandTResultDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _commandTResultDispatcher = commandTResultDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet("{OrderId:guid}")]
        public async Task<IActionResult> GetOrder([FromRoute] GetOrder query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return Ok(result);
        }

        [HttpGet("user/{UserId:guid}")]
        public async Task<IActionResult> GetUserOrders([FromRoute] GetBasicUserOrders query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(AddOrder command)
        {
            var result = await _commandTResultDispatcher.DispatchAsync(command);
            return Ok(result);
        }

        [HttpPost("orderline/add")]
        public async Task<IActionResult> AddOrderLine(AddOrderLine command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Ok();
        }

        [HttpPatch("cancel")]
        public async Task<IActionResult> Cancel(CancelOrder command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Ok();
        }

        [HttpPatch("delivery/change")]
        public async Task<IActionResult> ChangeDelivery(ChangeDeliveryDetails command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Ok();
        }

        [HttpPatch("complete")]
        public async Task<IActionResult> Complete(MoveToComplete command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Ok();
        }

        [HttpDelete("orderline/delete")]
        public async Task<IActionResult> DeleteOrderLine(DeleteOrderLine command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Ok();
        }

        [HttpPatch("pay")]
        public async Task<IActionResult> Pay(PayOrder command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Ok();
        }

        [HttpPatch("pending")]
        public async Task<IActionResult> Pending(MoveToPending command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Ok();
        }

        [HttpPatch("return")]
        public async Task<IActionResult> Return(ReturnOrder command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Ok();
        }
    }
}
