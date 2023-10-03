using SimpleShopD.Application.Commands.SharedDto;
using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Orders;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Domain.Shared.ValueObjects;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Orders.Add
{
    internal sealed class AddOrderHandler : ICommandTValueHandler<AddOrder, Guid>
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
           if(!await _userRepository.DoesExist(command.UserId))
                throw new UserDoesNotExistException(command.UserId.ToString());

            var address = command.OrderAddress.MapToDomainAddress();
            var fullname = MapFullname(command);

            var order = new Order(Guid.NewGuid(), command.UserId, address, fullname);
            command.OrderLines.ToList().ForEach(async x =>
            {
                if (!await _productRepository.DoesExist(x.ProductId))
                        throw new ProductDoesNotExistException(x.ProductId.ToString());
                order.AddOrderLine(Guid.NewGuid(), x.ProductId, x.Quantity, x.Price);
            });

            var id = await _orderRepository.AddAsync(order);
            return id;
        }

        private static Fullname MapFullname(AddOrder command)
            => new(command.FirstName, command.LastName);
    }
}
