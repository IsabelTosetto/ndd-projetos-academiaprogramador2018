using DonaLaura.Common.Tests.Base;
using DonaLaura.Common.Tests.Features;
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

        //[Test]
        //public void ProductSqlRepository_Save_ShouldBeOK()
        //{
        //    //Cenario
        //    Product product = ObjectMother.GetProduct();

        //    //Executa
        //    Product resultado = _repository.Save(product);

        //    //Verifica
        //    resultado.Should().NotBeNull();
        //    resultado.Id.Should().BeGreaterThan(0);
        //    resultado.Name.Should().Be(ObjectMother.GetProduct().Name);
        //    resultado.SalePrice.Should().Be(ObjectMother.GetProduct().SalePrice);
        //}

        //[Test]
        //public void ProductSqlRepository_Save_Name_NullOrEmpty_ShouldBeFail()
        //{
        //    //Cenario
        //    Product product = ObjectMother.GetProductWithInvalidName();

        //    //Executa
        //    Action comparison = () => _repository.Save(post);

        //    comparison.Should().Throw<PostMessageNullOrEmptyException>();
        //}

        [TearDown]
        public void TearDown()
        {
            _repository = null;
        }
    }
}
