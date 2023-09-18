using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Orders;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Doman.Shared.ValueObjects;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Orders.Add
{
    internal sealed class AddOrderHandler : ICommandHandler<AddOrder>
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

        public async Task HandleAsync(AddOrder command)
        {
            var user = await _userRepository.GetAsync(command.UserId)
                ?? throw new UserDoesNotExistException(command.UserId.ToString());

            var address = MapAddress(command.OrderAddress);
            var fullname = MapFullname(command);
            var lines = await MapLines(command);

            var order = new Order<Guid>(Guid.NewGuid(), DateTime.UtcNow, user, address, fullname, lines);
            await _orderRepository.AddAsync(order);
        }

        private async Task<List<OrderLine<Guid>>> MapLines(AddOrder command)
        {
            if (!await _productRepository.AreAllExists(command.OrderLines.Select(x => x.ProductId)))
                throw new ProductDoesNotExistException("Some products ids from collection does not exist.");

            var lines = command.OrderLines
                .Select((line, index) =>
                    new OrderLine<Guid>(
                        Guid.NewGuid(),
                        index + 1,
                        line.SalePrice,
                        line.Quantity,
                        line.ProductId))
                .ToList();

            return lines;
        }

        private static Fullname MapFullname(AddOrder command) 
            => new(command.FirstName, command.LastName);

        private static Address MapAddress(OrderAddress orderAddress) 
            => new(
                orderAddress.Country,
                orderAddress.City,
                orderAddress.ZipCode,
                orderAddress.Street,
                orderAddress.BuildingNumber);
    }
}
