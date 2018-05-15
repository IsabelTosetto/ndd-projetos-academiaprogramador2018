using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using DonaLaura.Domain.Features.Products;
using DonaLaura.Application.Features.Products;
using FluentAssertions;
using DonaLaura.Domain.Exceptions;
using DonaLaura.Common.Tests.Features;

namespace DonaLaura.Application.Tests.Features.Products
{
    [TestFixture]
    public class ProductServiceTest
    {
        private Mock<IProductRepository> _mockRepository;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IProductRepository>();
        }

        [Test]
        public void ProductService_Add_ShouldBeOK()
        {
            // Inicio Cenário

            //Modelo
            Product modelo = new Product()
            {
                Id = 1,
                Name = "Rice",
                SalePrice = 6,
                CostPrice = 4,
                Disponibility = true,
                FabricationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMonths(4)
            };
            //Mock
            _mockRepository.Setup(m => m.Save(modelo)).Returns(new Product() { Id = 1 });
            //Serviço
            ProductService service = new ProductService(_mockRepository.Object);
            // Fim Cenário

            //Executa
            Product resultado = service.Add(modelo);

            //Saída
            resultado.Should().NotBeNull();
            resultado.Id.Should().BeGreaterThan(0);
            _mockRepository.Verify(repository => repository.Save(modelo));
        }

        [Test]
        public void ProductService_Add_Message_NullOrEmpty_ShouldBeFail()
        {
            // Inicio Cenário

            //Modelo
            Product modelo = new Product()
            {
                Id = 1,
                Name = "",
                SalePrice = 6,
                CostPrice = 4,
                Disponibility = true,
                FabricationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMonths(4)
            };
            //Serviço
            ProductService service = new ProductService(_mockRepository.Object);
            // Fim Cenário

            //Executa
            Action comparison = () => service.Add(modelo);

            //Saída
            comparison.Should().Throw<ProductNameNullOrEmptyException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void ProductService_Add_Message_LessThan4_ShouldBeFail()
        {
            // Inicio Cenário

            //Modelo
            Product modelo = new Product()
            {
                Id = 1,
                Name = "Ric",
                SalePrice = 6,
                CostPrice = 4,
                Disponibility = true,
                FabricationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMonths(4)
            };
            //Serviço
            ProductService service = new ProductService(_mockRepository.Object);
            // Fim Cenário

            //Executa
            Action comparison = () => service.Add(modelo);

            //Saída
            comparison.Should().Throw<ProductNameLessThan4Exception>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void ProductService_Add_SalePrice_Null_ShouldBeFail()
        {
            // Inicio Cenário

            //Modelo
            Product modelo = new Product()
            {
                Id = 1,
                Name = "Rice",
                SalePrice = 0,
                CostPrice = 4,
                Disponibility = true,
                FabricationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMonths(4)
            };
            //Serviço
            ProductService service = new ProductService(_mockRepository.Object);
            // Fim Cenário

            //Executa
            Action comparison = () => service.Add(modelo);

            //Saída
            comparison.Should().Throw<ProductSalePriceNullException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void ProductService_Add_CostPrice_BiggerThanSalePrice_ShouldBeFail()
        {
            // Inicio Cenário

            //Modelo
            Product modelo = new Product()
            {
                Id = 1,
                Name = "Rice",
                SalePrice = 2,
                CostPrice = 4,
                Disponibility = true,
                FabricationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMonths(4)
            };
            //Serviço
            ProductService service = new ProductService(_mockRepository.Object);
            // Fim Cenário

            //Executa
            Action comparison = () => service.Add(modelo);

            //Saída
            comparison.Should().Throw<ProductCostPriceBiggerThanSalePriceException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void ProductService_Add_FabricationDate_BiggerThanDateNow_ShouldBeFail()
        {
            // Inicio Cenário

            //Modelo
            Product modelo = new Product()
            {
                Id = 1,
                Name = "Rice",
                SalePrice = 6,
                CostPrice = 4,
                Disponibility = true,
                FabricationDate = DateTime.Now.AddMonths(1),
                ExpirationDate = DateTime.Now.AddMonths(4)
            };
            //Serviço
            ProductService service = new ProductService(_mockRepository.Object);
            // Fim Cenário

            //Executa
            Action comparison = () => service.Add(modelo);

            //Saída
            comparison.Should().Throw<ProductDateOverFlowException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void ProductService_Add_ExpirationDate_LessThanFabricationDate_ShouldBeFail()
        {
            // Inicio Cenário

            //Modelo
            Product modelo = new Product()
            {
                Id = 1,
                Name = "Rice",
                SalePrice = 6,
                CostPrice = 4,
                Disponibility = true,
                FabricationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMonths(-4)
            };
            //Serviço
            ProductService service = new ProductService(_mockRepository.Object);
            // Fim Cenário

            //Executa
            Action comparison = () => service.Add(modelo);

            //Saída
            comparison.Should().Throw<ProductDateOverFlowException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void ProductService_Update_ShouldBeOK()
        {
            // Início Cenário

            //Modelo
            Product modelo = ObjectMother.GetProduct();
            modelo.Id = 1;
            //Mock
            _mockRepository.Setup(m => m.Update(modelo)).Returns(new Product()
            {
                Id = 1,
                Name = "Rice"
            });

            //Serviço
            ProductService service = new ProductService(_mockRepository.Object);
            // Fim Cenário


            //Executa
            Product resultado = service.Update(modelo);

            //Saída
            resultado.Should().NotBeNull();
            resultado.Id.Should().BeGreaterThan(0);
            resultado.Name.Should().Be("Rice");
            _mockRepository.Verify(repository => repository.Update(modelo));
        }

        [Test]
        public void ProductService_Update_Name_NullOrEmpty_ShouldBeFail()
        {
            // Início Cenário
            //Serviço
            _mockRepository = new Mock<IProductRepository>();
            ProductService service = new ProductService(_mockRepository.Object);
            // Fim Cenario

            //Executa
            Action comparison = () => service.Update(ObjectMother.GetProductWithInvalidName());
            //Saída
            comparison.Should().Throw<ProductNameNullOrEmptyException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void ProductService_Update_Invalid_Id_ShouldBeFail()
        {
            // Inicio Cenario

            //Serviço
            ProductService service = new ProductService(_mockRepository.Object);
            // Fim Cenario

            //Executa
            Action comparison = () => service.Update(ObjectMother.GetProduct());

            //Saída
            comparison.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void ProductService_Get_ShouldBeOk()
        {
            // Inicio Cenario

            //Mock
            _mockRepository.Setup(m => m.Get(3)).Returns(new Product()
            {
                Id = 3,
                Name = "Rice",
                SalePrice = 6,
                CostPrice = 4,
                Disponibility = true,
                FabricationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMonths(4)
            });
            
            //Serviço
            ProductService service = new ProductService(_mockRepository.Object);
            // Fim Cenario

            //Executa
            Product resultado = service.Get(3);

            //Saída
            resultado.Should().NotBeNull();
            _mockRepository.Verify(repository => repository.Get(3));
        }

        [Test]
        public void ProductService_Get_Invalid_Id_ShouldBeFail()
        {
            // Inicio Cenario

            //Serviço
            ProductService service = new ProductService(_mockRepository.Object);
            // Fim Cenario

            //Executa
            Action comparison = () => service.Get(0);

            //Saída
            comparison.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void ProductService_GetAll_ShouldBeOk()
        {
            // Inicio Cenario

            //Mock
            _mockRepository.Setup(m => m.GetAll()).Returns(new List<Product>());

            //Serviço
            ProductService service = new ProductService(_mockRepository.Object);
            // Fim Cenario

            //Executa
            IEnumerable<Product> resultado = service.GetAll();

            //Saída
            resultado.Should().NotBeNull();
            _mockRepository.Verify(repository => repository.GetAll());
        }

        [Test]
        public void ProductService_Delete_ShouldBeOk()
        {
            // Início Cenário

            //Modelo
            Product modelo = ObjectMother.GetProduct();
            modelo.Id = 1;
            //Mock
            _mockRepository.Setup(m => m.Delete(modelo));

            //Serviço
            ProductService service = new ProductService(_mockRepository.Object);
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
            ProductService service = new ProductService(_mockRepository.Object);
            // Fim Cenario

            //Executa
            Action comparison = () => service.Delete(ObjectMother.GetProduct());

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
