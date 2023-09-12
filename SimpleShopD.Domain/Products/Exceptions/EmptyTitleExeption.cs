using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShopD.Domain.Products.Exceptions
{
    public class EmptyTitleExeption : Exception
    {
        public EmptyTitleExeption(string message) : base(message) { }
    }
}
