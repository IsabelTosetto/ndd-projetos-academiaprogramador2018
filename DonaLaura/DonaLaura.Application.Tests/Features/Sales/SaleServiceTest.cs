using DonaLaura.Domain.Features.Sales;
using NUnit.Framework;
using System;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonaLaura.Domain.Features.Products;
using DonaLaura.Common.Tests.Features.Sales;
using DonaLaura.Application.Features.Sales;
using FluentAssertions;
using DonaLaura.Domain.Exceptions;

namespace DonaLaura.Application.Tests.Features.Sales
{
    [TestFixture]
    public class SaleServiceTest
    {
        private Mock<ISaleRepository> _mockRepository;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<ISaleRepository>();
        }

        [Test]
        public void ProductService_Add_ShouldBeOK()
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
                ExpirationDate = DateTime.Now.AddMonths(4)
            };

            //Mock
            _mockRepository.Setup(m => m.Save(modelo)).Returns(new Sale() { Id = 1 });
            //Serviço
            SaleService service = new SaleService(_mockRepository.Object);
            // Fim Cenário

            //Executa
            Sale resultado = service.Add(modelo);

            //Saída
            resultado.Should().NotBeNull();
            resultado.Id.Should().BeGreaterThan(0);
            _mockRepository.Verify(repository => repository.Save(modelo));
        }

        [Test]
        public void SaleService_Add_ClientName_NullOrEmpty_ShouldBeFail()
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
            //Serviço
            SaleService service = new SaleService(_mockRepository.Object);
            // Fim Cenário

            //Executa
            Action comparison = () => service.Add(modelo);

            //Saída
            comparison.Should().Throw<SaleClientNameNullOrEmptyException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void SaleService_Add_Product_Unavailable_ShouldBeFail()
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
            //Serviço
            SaleService service = new SaleService(_mockRepository.Object);
            // Fim Cenário

            //Executa
            Action comparison = () => service.Add(modelo);

            //Saída
            comparison.Should().Throw<SaleProductUnavailableException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void SaleService_Add_Product_ExpirationDate_ShouldBeFail()
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
            //Serviço
            SaleService service = new SaleService(_mockRepository.Object);
            // Fim Cenário

            //Executa
            Action comparison = () => service.Add(modelo);

            //Saída
            comparison.Should().Throw<SaleProductUnavailableException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void SaleService_Add_Quantity_LessThan1_ShouldBeFail()
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
            //Serviço
            SaleService service = new SaleService(_mockRepository.Object);
            // Fim Cenário

            //Executa
            Action comparison = () => service.Add(modelo);

            //Saída
            comparison.Should().Throw<SaleQuantityLessThan1Exception>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void SaleService_Update_ShouldBeOK()
        {
            // Início Cenário

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
                ExpirationDate = DateTime.Now.AddMonths(4)
            };
            modelo.Id = 1;
            //Mock
            _mockRepository.Setup(m => m.Update(modelo)).Returns(new Sale()
            {
                Id = 1,
                ClientName = "Isabel"
            });

            //Serviço
            SaleService service = new SaleService(_mockRepository.Object);
            // Fim Cenário

            //Executa
            Sale resultado = service.Update(modelo);

            //Saída
            resultado.Should().NotBeNull();
            resultado.Id.Should().BeGreaterThan(0);
            resultado.ClientName.Should().Be("Isabel");
            _mockRepository.Verify(repository => repository.Update(modelo));
        }

        [Test]
        public void SaleService_Update_Invalid_Id_ShouldBeFail()
        {
            // Inicio Cenario

            //Serviço
            SaleService service = new SaleService(_mockRepository.Object);
            // Fim Cenario

            //Executa
            Action comparison = () => service.Update(ObjectMother.GetSale());

            //Saída
            comparison.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void SaleService_Update_ClientName_NullOrEmpty_ShouldBeFail()
        {
            // Início Cenário
            //Serviço
            _mockRepository = new Mock<ISaleRepository>();
            SaleService service = new SaleService(_mockRepository.Object);
            // Fim Cenario

            //Executa
            Action comparison = () => service.Update(ObjectMother.GetSaleWithInvalidClientName());
            //Saída
            comparison.Should().Throw<SaleClientNameNullOrEmptyException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void SaleService_Update_Quantity_LessThan1_ShouldBeFail()
        {
            // Início Cenário
            //Serviço
            _mockRepository = new Mock<ISaleRepository>();
            SaleService service = new SaleService(_mockRepository.Object);
            // Fim Cenario

            //Executa
            Action comparison = () => service.Update(ObjectMother.GetSaleWithInvalidClientName());
            //Saída
            comparison.Should().Throw<SaleClientNameNullOrEmptyException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void SaleService_Get_ShouldBeOk()
        {
            // Inicio Cenario
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

            //Mock
            _mockRepository.Setup(m => m.Get(3)).Returns(modelo);
            
            //Serviço
            SaleService service = new SaleService(_mockRepository.Object);
            // Fim Cenario

            //Executa
            Sale resultado = service.Get(3);

            //Saída
            resultado.Should().NotBeNull();
            _mockRepository.Verify(repository => repository.Get(3));
        }

        [Test]
        public void SaleService_Get_Invalid_Id_ShouldBeFail()
        {
            // Inicio Cenario

            //Serviço
            SaleService service = new SaleService(_mockRepository.Object);
            // Fim Cenario

            //Executa
            Action comparison = () => service.Get(0);

            //Saída
            comparison.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void SaleService_GetAll_ShouldBeOk()
        {
            // Inicio Cenario

            //Mock
            _mockRepository.Setup(m => m.GetAll()).Returns(new List<Sale>());

            //Serviço
            SaleService service = new SaleService(_mockRepository.Object);
            // Fim Cenario

            //Executa
            IEnumerable<Sale> resultado = service.GetAll();

            //Saída
            resultado.Should().NotBeNull();
            _mockRepository.Verify(repository => repository.GetAll());
        }

        [Test]
        public void ProductService_Delete_ShouldBeOk()
        {
            // Início Cenário

            //Modelo
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
            //Mock
            _mockRepository.Setup(m => m.Delete(modelo));

            //Serviço
            SaleService service = new SaleService(_mockRepository.Object);
            // Fim Cenário

            //Executa
            service.Delete(modelo);

            //Saída
            _mockRepository.Verify(repository => repository.Delete(modelo));
        }

        [Test]
        public void ProductService_Delete_Invalid_Id_ShouldBeFail()
        {
            // Inicio Cenario

            //Serviço
            SaleService service = new SaleService(_mockRepository.Object);
            // Fim Cenario

            //Executa
            Action comparison = () => service.Delete(ObjectMother.GetSale());

            //Saída
            comparison.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [TearDown]
        public void TearDown()
        {
            _mockRepository = null;
        }
    }
}
