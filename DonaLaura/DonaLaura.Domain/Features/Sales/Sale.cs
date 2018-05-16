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
        public double Lucre
        {
            get
            {
                return CalculateLucre();
            }
        }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(ClientName))
                throw new SaleClientNameNullOrEmptyException();

            if (Quantity < 1)
                throw new SaleQuantityLessThan1Exception();

            if (Product.Disponibility == false)
                throw new SaleProductUnavailableException();

            if(Product.ExpirationDate < DateTime.Now)
                throw new SaleProductUnavailableException();
        }

        private double CalculateLucre()
        {
            return ((Product.SalePrice - Product.CostPrice) * Quantity);
        }
    }
}
