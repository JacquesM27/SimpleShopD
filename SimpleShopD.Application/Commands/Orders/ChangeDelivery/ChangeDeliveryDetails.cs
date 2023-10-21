using SimpleShopD.Shared.Abstractions.Commands;

namespace SimpleShopD.Application.Commands.Orders.ChangeDelivery
{
    public sealed record ChangeDeliveryDetails(Guid OrderId, Guid AddressId, string Firstname, string Lastname) : ICommand;
}
