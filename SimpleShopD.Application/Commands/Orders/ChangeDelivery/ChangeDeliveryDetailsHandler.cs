using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Domain.Shared.ValueObjects;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Orders.ChangeDelivery
{
    internal sealed class ChangeDeliveryDetailsHandler : ICommandHandler<ChangeDeliveryDetails>
    {
        private readonly IOrderRepository _orderRepository;

        public ChangeDeliveryDetailsHandler(IOrderRepository orderRepository)
            => _orderRepository = orderRepository;

        public async Task HandleAsync(ChangeDeliveryDetails command)
        {
            var order = await _orderRepository.GetAsync(command.OrderId)
               ?? throw new OrderDoesNotExistException(command.OrderId.ToString());

            var fullname = MapFullname(command.Firstname, command.Lastname);

            order.ChangeReceiverFullname(fullname);
            order.ChangeDeliveryAddress(command.AddressId);

            await _orderRepository.UpdateAsync(order);
        }

        private static Fullname MapFullname(string firstname, string lastname)
            => new(firstname, lastname);
    }
}
