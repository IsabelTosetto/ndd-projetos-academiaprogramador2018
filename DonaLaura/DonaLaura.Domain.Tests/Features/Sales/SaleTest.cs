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
using DonaLaura.Common.Tests.Features.Sales;

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
            sale = ObjectMother.GetSale();
            sale.Id = 1;
            sale.Product = _product.Object;

            _product.Object.Disponibility = true;
            _product.Object.ExpirationDate = DateTime.Now.AddMonths(4);

            //Executa
            sale.Validate();
        }

        [Test]
        public void Sale_Validate_ClienteName_NullOrEmpty_ShouldBeFail()
        {
            //Cenário
            sale = ObjectMother.GetSaleWithInvalidClientName();
            sale.Id = 1;
            sale.Product = _product.Object;

            //Executa
            Action comparison = sale.Validate;

            //Saída
            comparison.Should().Throw<SaleClientNameNullOrEmptyException>();
        }

        [Test]
        public void Sale_Validate_Quantity_LessThan1_ShouldBeFail()
        {
            //Cenário
            sale = ObjectMother.GetSaleWithInvalidQuantity();
            sale.Id = 1;
            sale.Product = _product.Object;

            //Executa
            Action comparison = sale.Validate;

            //Saída
            comparison.Should().Throw<SaleQuantityLessThan1Exception>();
        }

        [Test]
        public void Sale_Validate_Product_Unavailable_ShouldBeFail()
        {
            //Cenário
            sale = ObjectMother.GetSale();
            sale.Id = 1;
            _product.Object.Disponibility = false;
            sale.Product = _product.Object;

            //Executa
            Action comparison = sale.Validate;

            //Saída
            comparison.Should().Throw<SaleProductUnavailableException>();
        }


        [Test]
        public void Sale_Validate_Lucre_ShouldBeOk()
        {
            //Cenário
            sale.Id = 1;
            sale.Product = _product.Object;
            sale.ClientName = "Isabel";
            sale.Quantity = 2;

            _product.Object.SalePrice = 6;
            _product.Object.CostPrice = 2;
            
            

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
