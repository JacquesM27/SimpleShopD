using SimpleShopD.Application.Dto;
using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Shared.Abstractions.Queries;

namespace SimpleShopD.Application.Queries.Orders.Get
{
    internal class GetOrderHandler : IQueryHandler<GetOrder, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;

        public GetOrderHandler(IOrderRepository orderRepository, IUserRepository userRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        public async Task<OrderDto> HandleAsync(GetOrder query)
        {
            var order = await _orderRepository.GetAsync(query.OrderId)
                ?? throw new OrderDoesNotExistException(query.OrderId.ToString());

            return new OrderDto(order.Id,
                await MapAddress(order.UserId, order.AddressId),
                order.ReceiverFullname.Firstname,
                order.ReceiverFullname.Lastname,
                order.Status.Value.ToString(),
                await MapLines(order.OrderLines));
        }

        private async Task<IEnumerable<LineDto>> MapLines(ICollection<Domain.Orders.OrderLine> lines)
        {
            var products = await _productRepository.GetByIdsAsync(lines.Select(x => x.ProductId));

            return lines.Select(x =>
            {
                var product = products.Where(c => c.Id == x.ProductId).FirstOrDefault();
                return new LineDto(x.Id, x.No, product!.Title, x.Quantity, x.Price);
            });
        }

        private async Task<AddressDto> MapAddress(Guid userId, Guid addressId)
        {
            var address = await _userRepository.GetAddressAsync(userId, addressId) ?? 
                throw new AddressDoesNotExistException(addressId.ToString());

            return new AddressDto(address.Country, address.City, address.ZipCode, address.Street, address.BuildingNumber);
        }
    }
}
