using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Orders;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Domain.Shared.ValueObjects;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Orders.Add
{
    internal sealed class AddOrderHandler : ICommandTResultHandler<AddOrder, Guid>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;

        public AddOrderHandler(IOrderRepository orderRepository, IUserRepository userRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        public async Task<Guid> HandleAsync(AddOrder command)
        {
           if(!await _userRepository.DoesExistAsync(command.UserId))
                throw new UserDoesNotExistException(command.UserId.ToString());

            var fullname = MapFullname(command);

            var order = new Order(Guid.NewGuid(), command.UserId, command.OrderAddress, fullname);
            command.OrderLines.ToList().ForEach(async x =>
            {
                var price = await _productRepository.GetPrice(x.ProductId) ??
                        throw new ProductDoesNotExistException(x.ProductId.ToString());
                order.AddOrderLine(Guid.NewGuid(), x.ProductId, x.Quantity, price);
            });

            var id = await _orderRepository.AddAsync(order);
            return id;
        }

        private static Fullname MapFullname(AddOrder command)
            => new(command.FirstName, command.LastName);
    }
}
