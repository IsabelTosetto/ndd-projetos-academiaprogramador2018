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
        Sale sale;
        Mock<Product> _product;

        [SetUp]
        public void Setup()
        {
            sale = new Sale();
            _product = new Mock<Product>();
        }

        [Test]
        public void Sale_Valid_ShouldBeSuccess()
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
        public void Sale_Valid_ClienteName_NullOrEmpty_ShouldBeFail()
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
        public void Sale_Valid_Quantity_LessThan1_ShouldBeFail()
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

        //[Test]
        //public void Sale_Valid_Lucre_ShouldBeOk()
        //{
        //    //Cenário
        //    sale.Id = 1;
        //    sale.Product = _product.Object;
        //    sale.ClientName = "Isabel";
        //    sale.Quantity = 2;

        //    //Executa
        //    Action comparison = sale.Validate;

        //    //Saída

        //}

        [TearDown]
        public void TearDown()
        {
            sale = null;
            _product = null;
        }
    }
}
