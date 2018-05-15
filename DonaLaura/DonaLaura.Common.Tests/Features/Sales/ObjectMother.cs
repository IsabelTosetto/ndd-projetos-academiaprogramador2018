using DonaLaura.Domain.Features.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Common.Tests.Features.Sales
{
    public static partial class ObjectMother
    {
        public static Sale GetSale()
        {
            return new Sale()
            {
                ClientName = "Isabel",
                Quantity = 2
            };
        }

        public static Sale GetSaleWithInvalidClientName()
        {
            return new Sale()
            {
                Id = 1,
                ClientName = "",
                Quantity = 2
            };
        }

        public static Sale GetSaleWithInvalidQuantity()
        {
            return new Sale()
            {
                Id = 1,
                ClientName = "Isabel",
                Quantity = -1
            };
        }
    }
}
