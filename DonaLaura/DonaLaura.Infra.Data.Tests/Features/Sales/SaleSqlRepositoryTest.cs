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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Infra.Data.Tests.Features.Sales
{
    [TestFixture]
    public class SaleSqlRepositoryTest
    {
        private ISaleRepository _repository;

        [SetUp]
        public void Setup()
        {
            BaseSqlTest.SeedDatabase();
            _repository = new SaleSqlRepository();
        }

        [Test]
        public void SaleSqlRepository_Save_ShouldBeOK()
        {
            //Cenario
            Sale sale = ObjectMother.GetSale();
            sale.Product = new Product
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
            Sale resultado = _repository.Save(sale);

            //Verifica
            resultado.Should().NotBeNull();
            resultado.Id.Should().BeGreaterThan(0);
            resultado.ClientName.Should().Be(ObjectMother.GetSale().ClientName);
            resultado.Quantity.Should().Be(ObjectMother.GetSale().Quantity);
        }

        [Test]
        public void SaleSqlRepository_Save_ClientName_NullOrEmpty_ShouldBeFail()
        {
            //Cenario
            Sale sale = ObjectMother.GetSaleWithInvalidClientName();

            //Executa
            Action comparison = () => _repository.Save(sale);

            comparison.Should().Throw<SaleClientNameNullOrEmptyException>();
        }

        [Test]
        public void SaleSqlRepository_Save_Quantity_LessThan1_ShouldBeFail()
        {
            //Cenario
            Sale sale = ObjectMother.GetSaleWithInvalidQuantity();

            //Executa
            Action comparison = () => _repository.Save(sale);

            comparison.Should().Throw<SaleQuantityLessThan1Exception>();
        }

        [Test]
        public void SaleSqlRepository_Update_ShouldBeOK()
        {
            //Cenario
            Sale sale = ObjectMother.GetSale();
            sale.Product = new Product
            {
                Id = 1,
                Name = "Rice",
                SalePrice = 6,
                CostPrice = 2,
                Disponibility = true,
                FabricationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMonths(4)
            };
            sale.Id = 1;

            //Executa
            _repository.Update(sale);

            //Verifica
            Sale saleEdited = _repository.Get(sale.Id);
            saleEdited.ClientName.Should().Be(sale.ClientName);
        }

        [Test]
        public void SaleSqlRepository_Update_Invalid_Id_ShouldBeFail()
        {
            //Cenario
            Sale sale = ObjectMother.GetSale();
            sale.Product = new Product
            {
                Id = 1,
                Name = "Rice",
                SalePrice = 6,
                CostPrice = 2,
                Disponibility = true,
                FabricationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMonths(4)
            };
            sale.Id = 0;

            //Executa
            Action comparison = () => _repository.Update(sale);

            //Saída
            comparison.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void SaleSqlRepository_Update_ClientName_NullOrEmpty_ShouldBeFail()
        {
            //Cenario
            Sale sale = ObjectMother.GetSaleWithInvalidClientName();
            sale.Product = new Product
            {
                Id = 1,
                Name = "Rice",
                SalePrice = 6,
                CostPrice = 2,
                Disponibility = true,
                FabricationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMonths(4)
            };
            sale.Id = 1;

            //Executa
            Action comparison = () => _repository.Update(sale);

            comparison.Should().Throw<SaleClientNameNullOrEmptyException>();
        }

        [Test]
        public void SaleSqlRepository_Update_Quantity_LessThan1_ShouldBeFail()
        {
            //Cenario
            Sale sale = ObjectMother.GetSaleWithInvalidQuantity();
            sale.Product = new Product
            {
                Id = 1,
                Name = "Rice",
                SalePrice = 6,
                CostPrice = 2,
                Disponibility = true,
                FabricationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMonths(4)
            };
            sale.Id = 1;

            //Executa
            Action comparison = () => _repository.Update(sale);

            comparison.Should().Throw<SaleQuantityLessThan1Exception>();
        }
        
        [Test]
        public void SaleSqlRepository_Get_ShouldBeOK()
        {
            //Executa
            Sale resultado = _repository.Get(1);

            //Verifica
            resultado.Should().NotBeNull();
        }

        [Test]
        public void SaleSqlRepository_Get_Invalid_Id_ShouldBeFail()
        {
            //Executa
            Action comparison = () => _repository.Get(-1);

            //Saída
            comparison.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void SaleSqlRepository_GetAll_ShoulBeOK()
        {
            //Executa
            IEnumerable<Sale> resultado = _repository.GetAll();

            //Verifica
            resultado.Count().Should().BeGreaterThan(0);
        }

        [Test]
        public void SaleSqlRepository_Delete_ShoulBeOK()
        {
            //Cenario
            Sale sale = ObjectMother.GetSale();
            sale.Product = new Product
            {
                Id = 1,
                Name = "Rice",
                SalePrice = 6,
                CostPrice = 2,
                Disponibility = true,
                FabricationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMonths(4)
            };
            sale.Id = 1;

            //Executa
            _repository.Delete(sale);
            Sale deleteObject = _repository.Get(1);

            //Verifica
            deleteObject.Should().BeNull();
        }

        [Test]
        public void SaleSqlRepository_Delete_ShoulBeFail()
        {
            //Cenario
            Sale sale = ObjectMother.GetSale();
            sale.Product = new Product
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
            Action comparison = () => _repository.Delete(sale);

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
