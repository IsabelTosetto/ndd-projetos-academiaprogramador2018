using DonaLaura.Domain.Common;
using DonaLaura.Domain.Features.Products;
using System;

namespace DonaLaura.Domain.Features.Sales
{
    public class Sale : Entity
    {
        public Product Product { get; set; }
        public string ClientName { get; set; }
        public int Quantity { get; set; }
        public double Lucre { get; set; }

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
