using DonaLaura.Domain.Features.Products;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Domain.Tests.Features.Products
{
    [TestFixture]
    public class ProductTest
    {
        Product product;

        [SetUp]
        public void Setup()
        {
            product = new Product();
        }

        [Test]
        public void Product_Valid_ShouldBeSuccess()
        {
            product.Id = 1;
            product.Name = "Rice";
            product.SalePrice = 6;
            product.CostPrice = 4;
            product.Disponibility = true;
            product.FabricationDate = DateTime.Now;
            product.ExpirationDate = DateTime.Now.AddMonths(4);

            product.Validate();
        }

        [Test]
        public void Product_Name_NullOrEmpty_ShouldBeFail()
        {
            product.Id = 1;
            product.Name = "";
            product.SalePrice = 6;
            product.CostPrice = 4;
            product.Disponibility = true;
            product.FabricationDate = DateTime.Now;
            product.ExpirationDate = DateTime.Now.AddMonths(4);

            Action comparison = product.Validate;

            comparison.Should().Throw<ProductNameNullOrEmptyException>();
        }

        [Test]
        public void Product_Name_LessThan4_ShouldBeFail()
        {
            product.Id = 1;
            product.Name = "Ric";
            product.SalePrice = 6;
            product.CostPrice = 4;
            product.Disponibility = true;
            product.FabricationDate = DateTime.Now;
            product.ExpirationDate = DateTime.Now.AddMonths(4);

            Action comparison = product.Validate;

            comparison.Should().Throw<ProductNameLessThan4Exception>();
        }

        [Test]
        public void Product_SalePrice_Null_ShouldBeFail()
        {
            product.Id = 1;
            product.Name = "Rice";
            product.SalePrice = 0;
            product.CostPrice = 4;
            product.Disponibility = true;
            product.FabricationDate = DateTime.Now;
            product.ExpirationDate = DateTime.Now.AddMonths(4);

            Action comparison = product.Validate;

            comparison.Should().Throw<ProductSalePriceNullException>();
        }

        [Test]
        public void Product_CostPrice_BiggerThanSalePrice_ShouldBeFail()
        {
            product.Id = 1;
            product.Name = "Rice";
            product.SalePrice = 2;
            product.CostPrice = 4;
            product.Disponibility = true;
            product.FabricationDate = DateTime.Now;
            product.ExpirationDate = DateTime.Now.AddMonths(4);

            Action comparison = product.Validate;

            comparison.Should().Throw<ProductCostPriceBiggerThanSalePriceException>();
        }

        [Test]
        public void Product_FabricationDate_BiggerThanDateNow_ShouldBeFail()
        {
            product.Id = 1;
            product.Name = "Rice";
            product.SalePrice = 6;
            product.CostPrice = 4;
            product.Disponibility = true;
            product.FabricationDate = DateTime.Now.AddMonths(1);
            product.ExpirationDate = DateTime.Now.AddMonths(4);

            Action comparison = product.Validate;

            comparison.Should().Throw<ProductDateOverFlowException>();
        }

        [Test]
        public void Product_ExpirationDate_LessThanFabricationDate_ShouldBeFail()
        {
            product.Id = 1;
            product.Name = "Rice";
            product.SalePrice = 6;
            product.CostPrice = 4;
            product.Disponibility = true;
            product.FabricationDate = DateTime.Now;
            product.ExpirationDate = DateTime.Now.AddMonths(-1);

            Action comparison = product.Validate;

            comparison.Should().Throw<ProductDateOverFlowException>();
        }

        [TearDown]
        public void TearDown()
        {
            product = null;
        }
    }
}
