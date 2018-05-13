using DonaLaura.Domain.Common;
using DonaLaura.Domain.Features.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Domain.Features.Sales
{
    public class Sale : Entity
    {
        private Product Product { get; set; }
        private string ClientName { get; set; }
        private int Quantity { get; set; }
        private double Lucre { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
