using DonaLaura.Domain.Common;
using System;

namespace DonaLaura.Domain.Features.Products
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public double SalePrice { get; set; }
        public double CostPrice { get; set; }
        public bool Disponibility { get; set; }
        public DateTime FabricationDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(Name))
                throw new ProductNameNullOrEmptyException();

            if (Name.Length < 4)
                throw new ProductNameLessThan4Exception();

            if (SalePrice < 1)
                throw new ProductSalePriceNullException();

            if (!ValidateCostPrice())
                throw new ProductCostPriceBiggerThanSalePriceException();

            if (!CompareDateSmallerCurrent(FabricationDate))
                throw new ProductDateOverFlowException();

            if (!ValidateExpirationDate())
                throw new ProductDateOverFlowException();
        }


        private bool ValidateCostPrice()
        {
            if(CostPrice < SalePrice)
            {
                return true;
            }
            return false;
        }

        private bool CompareDateSmallerCurrent(DateTime dt)
        {
            int result = DateTime.Compare(dt, DateTime.Now);
            if (result <= 0)
            {
                return true;
            }

            return false;
        }

        private bool ValidateExpirationDate()
        {
            int result = DateTime.Compare(FabricationDate, ExpirationDate);
            if (result <= 0)
            {
                return true;
            }

            return false;
        }
    }
}
