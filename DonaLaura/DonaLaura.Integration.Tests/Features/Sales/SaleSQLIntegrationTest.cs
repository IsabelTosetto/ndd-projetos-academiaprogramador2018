using DonaLaura.Application.Features.Sales;
using DonaLaura.Common.Tests.Base;
using DonaLaura.Common.Tests.Features.Sales;
using DonaLaura.Domain.Exceptions;
using DonaLaura.Domain.Features.Products;
using DonaLaura.Domain.Features.Sales;
using DonaLaura.Infra.Data.Features.Sales;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DonaLaura.Integration.Tests.Features.Sales
{
    [TestFixture]
    public class SaleSQLIntegrationTest
    {
        private SaleService _service;
        private ISaleRepository _repository;

        [SetUp]
        public void Setup()
        {
            BaseSqlTest.SeedDatabase();
            _repository = new SaleSqlRepository();
            _service = new SaleService(_repository);
        }

        [Test]
        public void SaleSQLIntegration_Add_ShouldBeOk()
        {
            //Cenario
            Sale modelo = ObjectMother.GetSale();
            modelo.Product = new Product
            {
                Id = 1,
                Name = "Rice",
                SalePrice = 6,
                CostPrice = 2,
                Disponibility = true,
                FabricationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMonths(4)
            };

            //Executa
            Sale sale = _service.Add(modelo);

            //Saída
            sale.Id.Should().BeGreaterThan(0);

            var last = _service.Get(sale.Id);
            last.Should().NotBeNull();

            var sales = _service.GetAll();
            sales.Count().Should().Be(2);
        }

        [Test]
        public void SaleSQLIntegration_Add_ClientName_NullOrEmpty_ShouldBeFail()
        {
            // Inicio Cenário

            //Modelo
            Sale modelo = ObjectMother.GetSaleWithInvalidClientName();
            modelo.Product = new Product
            {
                Id = 1,
                Name = "Rice",
                SalePrice = 6,
                CostPrice = 2,
                Disponibility = true,
                FabricationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMonths(4)
            };
            //Executa
            Action comparison = () => _service.Add(modelo);

            //Saída
            comparison.Should().Throw<SaleClientNameNullOrEmptyException>();
        }

        [Test]
        public void SaleSQLIntegration_Add_Product_Unavailable_ShouldBeFail()
        {
            // Inicio Cenário

            //Modelo
            Sale modelo = ObjectMother.GetSale();
            modelo.Product = new Product
            {
                Id = 1,
                Name = "Rice",
                SalePrice = 6,
                CostPrice = 2,
                Disponibility = false,
                FabricationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMonths(4)
            };
            //Executa
            Action comparison = () => _service.Add(modelo);

            //Saída
            comparison.Should().Throw<SaleProductUnavailableException>();
        }

        [Test]
        public void SaleSQLIntegration_Add_Product_ExpirationDate_ShouldBeFail()
        {
            // Inicio Cenário

            //Modelo
            Sale modelo = ObjectMother.GetSale();
            modelo.Product = new Product
            {
                Id = 1,
                Name = "Rice",
                SalePrice = 6,
                CostPrice = 2,
                Disponibility = true,
                FabricationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMonths(-1)
            };
            //Executa
            Action comparison = () => _service.Add(modelo);

            //Saída
            comparison.Should().Throw<SaleProductUnavailableException>();
        }

        [Test]
        public void SaleSQLIntegration_Add_Quantity_LessThan1_ShouldBeFail()
        {
            // Inicio Cenário

            //Modelo
            Sale modelo = ObjectMother.GetSaleWithInvalidQuantity();
            modelo.Product = new Product
            {
                Id = 1,
                Name = "Rice",
                SalePrice = 6,
                CostPrice = 2,
                Disponibility = true,
                FabricationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMonths(4)
            };
            //Executa
            Action comparison = () => _service.Add(modelo);

            //Saída
            comparison.Should().Throw<SaleQuantityLessThan1Exception>();
        }

        [Test]
        public void SaleSQLIntegration_Update_ShouldBeOk()
        {
            //Cenário
            Sale modelo = ObjectMother.GetSale();
            modelo.Id = 1;
            modelo.Product = new Product
            {
                Id = 1,
                Name = "Rice",
                SalePrice = 6,
                CostPrice = 2,
                Disponibility = true,
                FabricationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMonths(4)
            };

            //Executa
            Sale sale = _service.Update(modelo);

            //Saída
            sale.Should().NotBeNull();
            sale.Id.Should().Be(modelo.Id);
            sale.ClientName.Should().Be(modelo.ClientName);
        }

        [Test]
        public void SaleSQLIntegration_Update_Invalid_Id_ShouldBeFail()
        {
            //Cenário
            Sale modelo = ObjectMother.GetSale();
            modelo.Product = new Product
            {
                Id = 1,
                Name = "Rice",
                SalePrice = 6,
                CostPrice = 2,
                Disponibility = true,
                FabricationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMonths(4)
            };

            //Executa
            Action comparison = () => _service.Update(modelo);

            //Saída
            comparison.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void SaleSQLIntegration_Update_ClientNameNullOrEmpty_ShouldBeFail()
        {
            //Cenário
            Sale modelo = ObjectMother.GetSaleWithInvalidClientName();
            modelo.Id = 1;

            //Executa
            Action comparison = () => _service.Update(modelo);

            //Saída
            comparison.Should().Throw<SaleClientNameNullOrEmptyException>();
        }

        [Test]
        public void SaleSQLIntegration_Update_Invalid_IdClienteName_ShouldBeFail()
        {
            //Cenário
            Sale modelo = ObjectMother.GetSaleWithInvalidClientName();
            modelo.Id = 1;

            //Executa
            Action comparison = () => _service.Update(modelo);

            //Saída
            comparison.Should().Throw<SaleClientNameNullOrEmptyException>();
        }

        [Test]
        public void SaleSQLIntegration_Get_ShouldBeOk()
        {
            //Executa
            Sale sale = _service.Get(1);

            //Saída
            sale.Should().NotBeNull();

            List<Sale> sales = (List<Sale>)_service.GetAll();
            var found = sales.Find(x => x.Id == sale.Id);
            sale.Id.Should().Be(found.Id);
        }

        [Test]
        public void SaleSQLIntegration_Get_ShouldBeFail()
        {
            //Executa
            Sale sale = _service.Get(2);

            //Saída
            sale.Should().BeNull();
        }

        [Test]
        public void SaleSQLIntegration_Get_Invalid_Id_ShouldBeFail()
        {
            //Executa
            Action comparison = () => _service.Get(-1);

            //Saída
            comparison.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void SaleSQLIntegration_GetAll_ShouldBeOk()
        {
            //Executa
            List<Sale> sales = _service.GetAll() as List<Sale>;

            //Saída
            sales.Should().NotBeNull();
            sales.Count.Should().BeGreaterThan(0);
        }

        [Test]
        public void SaleSQLIntegration_Delete_ShouldBeOk()
        {
            //Executa
            Sale modelo = ObjectMother.GetSale();
            modelo.Id = 1;
            _service.Delete(modelo);

            //Saída
            Sale sale = _service.Get(1);
            sale.Should().BeNull();

            List<Sale> sales = _service.GetAll() as List<Sale>;
            sales.Count().Should().Be(0);
        }

        [Test]
        public void SaleSQLIntegration_Delete_ShouldBeFail()
        {
            //Executa
            Action comparison = () => _service.Delete(ObjectMother.GetSale());

            //Saída
            comparison.Should().Throw<IdentifierUndefinedException>();
        }

        [TearDown]
        public void TearDown()
        {
            _repository = null;
            _service = null;
        }
    }
}