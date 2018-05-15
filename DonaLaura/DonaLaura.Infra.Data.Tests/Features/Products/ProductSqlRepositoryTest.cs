using DonaLaura.Common.Tests.Base;
using DonaLaura.Common.Tests.Features;
using DonaLaura.Domain.Exceptions;
using DonaLaura.Domain.Features.Products;
using DonaLaura.Infra.Data.Features.Products;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Infra.Data.Tests.Features.Products
{
    [TestFixture]
    public class ProductSqlRepositoryTest
    {
        private IProductRepository _repository;

        [SetUp]
        public void Setup()
        {
            BaseSqlTest.SeedDatabase();
            _repository = new ProductSqlRepository();
        }

        [Test]
        public void ProductSqlRepository_Save_ShouldBeOK()
        {
            //Cenario
            Product product = ObjectMother.GetProduct();

            //Executa
            Product resultado = _repository.Save(product);

            //Verifica
            resultado.Should().NotBeNull();
            resultado.Id.Should().BeGreaterThan(0);
            resultado.Name.Should().Be(ObjectMother.GetProduct().Name);
            resultado.SalePrice.Should().Be(ObjectMother.GetProduct().SalePrice);
        }

        [Test]
        public void ProductSqlRepository_Save_Name_NullOrEmpty_ShouldBeFail()
        {
            //Cenario
            Product product = ObjectMother.GetProductWithInvalidName();

            //Executa
            Action comparison = () => _repository.Save(product);

            comparison.Should().Throw<ProductNameNullOrEmptyException>();
        }

        [Test]
        public void ProductSqlRepository_Save_CostPrice_BiggerThanSalePrice_ShouldBeFail()
        {
            //Cenario
            Product product = ObjectMother.GetProductWithInvalidCostPrice();

            //Executa
            Action comparison = () => _repository.Save(product);

            comparison.Should().Throw<ProductCostPriceBiggerThanSalePriceException>();
        }

        [Test]
        public void ProductSqlRepository_Save_ExpirationDate_LessFabricationDate_ShouldBeFail()
        {
            //Cenario
            Product product = ObjectMother.GetProductWithInvalidExpirationDate();

            //Executa
            Action comparison = () => _repository.Save(product);

            comparison.Should().Throw<ProductDateOverFlowException>();
        }

        [Test]
        public void ProductSqlRepository_Update_ShouldBeOK()
        {
            //Cenario
            Product product = ObjectMother.GetProduct();
            product.Id = 1;

            //Executa
            _repository.Update(product);

            //Verifica
            Product productEdited = _repository.Get(product.Id);
            productEdited.Name.Should().Be(product.Name);
        }

        [Test]
        public void ProductSqlRepository_Update_Invalid_Id_ShouldBeFail()
        {
            //Cenario
            Product product = ObjectMother.GetProduct();
            product.Id = 0;
            
            //Executa
            Action comparison = () => _repository.Update(product);

            //Saída
            comparison.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void ProductSqlRepository_Update_Name_NullOrEmpty_ShouldBeFail()
        {
            //Cenario
            Product product = ObjectMother.GetProductWithInvalidName();
            product.Id = 1;

            //Executa
            Action comparison = () => _repository.Update(product);

            comparison.Should().Throw<ProductNameNullOrEmptyException>();
        }

        [Test]
        public void ProductSqlRepository_Get_ShouldBeOK()
        {
            //Executa
            Product resultado = _repository.Get(1);

            //Verifica
            resultado.Should().NotBeNull();
        }

        [Test]
        public void ProductSqlRepository_Get_Invalid_Id_ShouldBeFail()
        {
            //Executa
            Action comparison = () => _repository.Get(-1);

            //Saída
            comparison.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void ProductSqlRepository_GetAll_ShoulBeOK()
        {
            //Executa
            IEnumerable<Product> resultado = _repository.GetAll();

            //Verifica
            resultado.Count().Should().BeGreaterThan(0);
        }

        [Test]
        public void ProductSqlRepository_Delete_ShoulBeOK()
        {
            //Cenario
            Product product = ObjectMother.GetProduct();
            product.Id = 1;

            //Executa
            _repository.Delete(product);
            Product deleteObject = _repository.Get(1);

            //Verifica
            deleteObject.Should().BeNull();
        }

        [Test]
        public void ProductSqlRepository_Delete_ShoulBeFail()
        {
            //Cenario
            Product post = ObjectMother.GetProduct();

            //Executa
            Action comparison = () => _repository.Delete(post);

            //Saída
            comparison.Should().Throw<IdentifierUndefinedException>();
        }

        [TearDown]
        public void TearDown()
        {
            _repository = null;
        }
    }
}
