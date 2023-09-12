using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShopD.Domain.Products.Exceptions
{
    public class IncorrectPriceValueException : Exception
    {
        public IncorrectPriceValueException(string message) : base(message) { }
    }
}
