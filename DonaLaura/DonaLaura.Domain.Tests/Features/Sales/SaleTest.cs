using DonaLaura.Domain.Features.Sales;
using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonaLaura.Domain.Features.Products;
using FluentAssertions;

namespace DonaLaura.Domain.Tests.Features.Sales
{
    [TestFixture]
    public class SaleTest
    {
        public Sale sale;
        public Mock<Product> _product;

        [SetUp]
        public void Setup()
        {
            sale = new Sale();
            _product = new Mock<Product>();
        }

        [Test]
        public void Sale_Validate_ShouldBeSuccess()
        {
            //Cenário
            sale.Id = 1;
            sale.Product = _product.Object;
            sale.ClientName = "Isabel";
            sale.Quantity = 2;

            //Executa
            sale.Validate();
        }

        [Test]
        public void Sale_Validate_ClienteName_NullOrEmpty_ShouldBeFail()
        {
            //Cenário
            sale.Id = 1;
            sale.Product = _product.Object;
            sale.ClientName = "";
            sale.Quantity = 2;

            //Executa
            Action comparison = sale.Validate;

            //Saída
            comparison.Should().Throw<SaleClientNameNullOrEmptyException>();
        }

        [Test]
        public void Sale_Validate_Quantity_LessThan1_ShouldBeFail()
        {
            //Cenário
            sale.Id = 1;
            sale.Product = _product.Object;
            sale.ClientName = "Isabel";
            sale.Quantity = -2;

            //Executa
            Action comparison = sale.Validate;

            //Saída
            comparison.Should().Throw<SaleQuantityLessThan1Exception>();
        }

        [Test]
        public void Sale_Validate_Lucre_ShouldBeOk()
        {
            //Cenário
            sale.Id = 1;
            sale.Product = _product.Object;
            sale.ClientName = "Isabel";
            sale.Quantity = 2;

            _product.Setup(salePrice => salePrice.SalePrice).Returns(6);
            _product.Setup(costPrice => costPrice.CostPrice).Returns(2);
            
            //Saída
            sale.Lucre.Should().Be(8);
        }

        [TearDown]
        public void TearDown()
        {
            sale = null;
            _product = null;
        }
    }
}
