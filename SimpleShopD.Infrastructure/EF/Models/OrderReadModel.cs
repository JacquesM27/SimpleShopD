using SimpleShopD.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShopD.Infrastructure.EF.Models
{
    internal sealed class OrderReadModel
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public Guid UserId { get; set; }
        public AddressReadModel DeliveryAddress { get; set; }
        public string ReceiverFirstName { get; set; }
        public string ReceiverLastName { get; set; }
        public OrderStatus StatusOfOrder { get; set; }
        public IList<OrderLineReadModel> OrderLines { get; set; }
    }
}
