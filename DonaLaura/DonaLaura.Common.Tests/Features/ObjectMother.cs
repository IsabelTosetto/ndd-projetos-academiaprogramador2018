using DonaLaura.Domain.Features.Products;
using System;


namespace DonaLaura.Common.Tests.Features
{
    public static partial class ObjectMother
    {
        public static Product GetProduct()
        {
            return new Product()
            {
                Name = "Rice",
                SalePrice = 6,
                CostPrice = 4,
                Disponibility = true,
                FabricationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMonths(4)
            };
        }

        public static Product GetProductComplete()
        {
            return new Product()
            {
                Id = 1,
                Name = "Rice",
                SalePrice = 6,
                CostPrice = 4,
                Disponibility = true,
                FabricationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMonths(4)
            };
        }

        public static Product GetProductWithInvalidName()
        {
            return new Product()
            {
                Id = 1,
                Name = "",
                SalePrice = 6,
                CostPrice = 4,
                Disponibility = true,
                FabricationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMonths(4)
            };
        }

        public static Product GetProductWithInvalidCostPrice()
        {
            return new Product()
            {
                Id = 1,
                Name = "Rice",
                SalePrice = 2,
                CostPrice = 4,
                Disponibility = true,
                FabricationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMonths(4)
            };
        }

        public static Product GetProductWithInvalidExpirationDate()
        {
            return new Product()
            {
                Id = 1,
                Name = "Rice",
                SalePrice = 6,
                CostPrice = 4,
                Disponibility = true,
                FabricationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMonths(-4)
            };
        }
    }
}