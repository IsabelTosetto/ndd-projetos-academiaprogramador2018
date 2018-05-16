using DonaLaura.Application.Features.Products;
using DonaLaura.Common.Tests.Base;
using DonaLaura.Common.Tests.Features.Products;
using DonaLaura.Domain.Exceptions;
using DonaLaura.Domain.Features.Products;
using DonaLaura.Infra.Data.Features.Products;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DonaLaura.Integration.Tests.Features.Products
{
    [TestFixture]
    public class ProductSQLIntegrationTest
    {
        private ProductService _service;
        private IProductRepository _repository;

        [SetUp]
        public void Setup()
        {
            BaseSqlTest.SeedDatabase();
            _repository = new ProductSqlRepository();
            _service = new ProductService(_repository);
        }

        [Test]
        public void ProductSQLIntegration_Add_ShouldBeOk()
        {
            //Executa
            Product product = _service.Add(ObjectMother.GetProduct());

            //Saída
            product.Id.Should().BeGreaterThan(0);

            var last = _service.Get(product.Id);
            last.Should().NotBeNull();

            var products = _service.GetAll();
            products.Count().Should().Be(2);
        }

        [Test]
        public void ProductSQLIntegration_Add_NameNullOrEmpty_ShouldBeFail()
        {
            //Executa
            Action comparison = () => _service.Add(ObjectMother.GetProductWithInvalidName());

            //Saída
            comparison.Should().Throw<ProductNameNullOrEmptyException>();
        }

        [Test]
        public void ProductSQLIntegration_Add_LessThan4_ShouldBeFail()
        {
            //Executa
            Action comparison = () => _service.Add(new Product()
            {
                Id = 1,
                Name = "Ric",
                SalePrice = 6,
                CostPrice = 4,
                Disponibility = true,
                FabricationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMonths(4)
            });

            //Saída
            comparison.Should().Throw<ProductNameLessThan4Exception>();
        }

        [Test]
        public void ProductSQLIntegration_Add_SalePrice_Null_ShouldBeFail()
        {
            //Executa
            Action comparison = () => _service.Add(new Product()
            {
                Id = 1,
                Name = "Rice",
                CostPrice = 4,
                Disponibility = true,
                FabricationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMonths(4)
            });

            //Saída
            comparison.Should().Throw<ProductSalePriceNullException>();
        }

        [Test]
        public void ProductSQLIntegration_Add_CostPrice_BiggerThanSalePrice_ShouldBeFail()
        {
            //Executa
            Action comparison = () => _service.Add(ObjectMother.GetProductWithInvalidCostPrice());

            //Saída
            comparison.Should().Throw<ProductCostPriceBiggerThanSalePriceException>();
        }

        [Test]
        public void ProductSQLIntegration_Add_FabricationDate_BiggerThanDateNow_ShouldBeFail()
        {
            //Executa
            Action comparison = () => _service.Add(new Product
            {
                Id = 1,
                Name = "Rice",
                SalePrice = 6,
                CostPrice = 4,
                Disponibility = true,
                FabricationDate = DateTime.Now.AddMonths(1),
                ExpirationDate = DateTime.Now.AddMonths(4)
            });

            //Saída
            comparison.Should().Throw<ProductDateOverFlowException>();
        }

        [Test]
        public void ProductSQLIntegration_Add_ExpirationDate_LessThanFabricationDate_ShouldBeFail()
        {
            //Executa
            Action comparison = () => _service.Add(ObjectMother.GetProductWithInvalidExpirationDate());

            //Saída
            comparison.Should().Throw<ProductDateOverFlowException>();
        }

        [Test]
        public void ProductSQLIntegration_Update_ShouldBeOk()
        {
            //Cenário
            Product modelo = ObjectMother.GetProduct();
            modelo.Id = 1;

            //Executa
            Product product = _service.Update(modelo);

            //Saída
            product.Should().NotBeNull();
            product.Id.Should().Be(modelo.Id);
            product.Name.Should().Be(modelo.Name);
        }

        [Test]
        public void ProductSQLIntegration_Update_Invalid_Id_ShouldBeFail()
        {
            //Cenário
            Product modelo = ObjectMother.GetProduct();

            //Executa
            Action comparison = () => _service.Update(modelo);

            //Saída
            comparison.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void ProductSQLIntegration_Update_MessageNullOrEmpty_ShouldBeFail()
        {
            //Cenário
            Product modelo = ObjectMother.GetProductWithInvalidName();
            modelo.Id = 1;

            //Executa
            Action comparison = () => _service.Update(modelo);

            //Saída
            comparison.Should().Throw<ProductNameNullOrEmptyException>();
        }

        [Test]
        public void ProductSQLIntegration_Update_ExpirationDate_LessThanFabricationDate_ShouldBeFail()
        {
            //Cenário
            Product modelo = ObjectMother.GetProductWithInvalidExpirationDate();
            modelo.Id = 1;

            //Executa
            Action comparison = () => _service.Update(modelo);

            //Saída
            comparison.Should().Throw<ProductDateOverFlowException>();
        }

        [Test]
        public void ProductSQLIntegration_Get_ShouldBeOk()
        {
            //Executa
            Product product = _service.Get(1);

            //Saída
            product.Should().NotBeNull();

            List<Product> products = (List<Product>)_service.GetAll();
            var found = products.Find(x => x.Id == product.Id);
            product.Id.Should().Be(found.Id);
        }

        [Test]
        public void ProductSQLIntegration_Get_ShouldBeFail()
        {
            //Executa
            Product product = _service.Get(2);

            //Saída
            product.Should().BeNull();
        }

        [Test]
        public void ProductSQLIntegration_Get_Invalid_Id_ShouldBeFail()
        {
            //Executa
            Action comparison = () => _service.Get(-1);

            //Saída
            comparison.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void ProductSQLIntegration_GetAll_ShouldBeOk()
        {
            //Executa
            List<Product> posts = _service.GetAll() as List<Product>;

            //Saída
            posts.Should().NotBeNull();
            posts.Count.Should().BeGreaterThan(0);
        }

        [Test]
        public void ProductSQLIntegration_Delete_ShouldBeOk()
        {
            //Executa
            Product modelo = ObjectMother.GetProduct();
            modelo.Id = 1;
            _service.Delete(modelo);

            //Saída
            Product product = _service.Get(1);
            product.Should().BeNull();

            List<Product> products = _service.GetAll() as List<Product>;
            products.Count().Should().Be(0);
        }

        [Test]
        public void ProductSQLIntegration_Delete_ShouldBeFail()
        {
            //Executa
            Action comparison = () => _service.Delete(ObjectMother.GetProduct());

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