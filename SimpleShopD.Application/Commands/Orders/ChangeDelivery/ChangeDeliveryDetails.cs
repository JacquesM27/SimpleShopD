using SimpleShopD.Application.Commands.SharedDto;
using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Orders.ChangeDelivery
{
    public sealed record ChangeDeliveryDetails(Guid OrderId, Address OrderAddress, string Firstname, string Lastname) : ICommand;
}
