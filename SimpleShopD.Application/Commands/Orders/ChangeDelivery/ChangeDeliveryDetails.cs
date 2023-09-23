using SimpleShopD.Application.Commands.Orders.SharedDto;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Orders.ChangeDelivery
{
    public sealed record ChangeDeliveryDetails(Guid OrderId, OrderAddress OrderAddress, string Firstname, string Lastname) : ICommand;
}
