using FluentAssertions;
using Moq;
using NUnit.Framework;
using SalaDeReuniao.Common.Tests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerceiroReforco.Application.Features.Employees;
using TerceiroReforco.Domain.Exceptions;
using TerceiroReforco.Domain.Features.Employees;

namespace SalaDeReuniao.Application.Tests.Features.Employees
{
    [TestFixture]
    public class EmployeeServiceTest
    {
        private IEmployeeService _service;
        private Mock<IEmployeeRepository> _mockRepository;

        [SetUp]
        public void Initialize()
        {
            _mockRepository = new Mock<IEmployeeRepository>();
            _service = new EmployeeService(_mockRepository.Object);
        }

        [Test]
        public void EmployeeService_Save_ShouldBeOk()
        {
            // Cenário
            Employee employee = ObjectMother.GetEmployee();

            _mockRepository
                .Setup(m => m.Save(employee))
                .Returns(new Employee { Id = 1 });

            // Ação
            Employee result = _service.Add(employee);

            // Verifica
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            _mockRepository.Verify(pr => pr.Save(employee));
        }

        [Test]
        public void EmployeeService_Save_InvalidEmptyOrNullName_ShouldBeFail()
        {
            // Cenário
            Employee employee = ObjectMother.GetEmployeeWithEmptyName();

            // Ação
            Action executeAction = () => _service.Add(employee);

            // Verifica
            executeAction.Should().Throw<EmployeeEmptyOrNullNameException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void EmployeeService_Update_ShouldBeOk()
        {
            // Cenário
            Employee employee = ObjectMother.GetEmployee();
            employee.Id = 1;

            _mockRepository
                .Setup(m => m.Update(employee))
                .Returns(new Employee { Id = 1, Name = "Luciane" });

            // Ação
            Employee result = _service.Update(employee);

            // Verifica
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            _mockRepository.Verify(pr => pr.Update(employee));
        }

        [Test]
        public void EmployeeService_Update_InvalidId_ShouldBeFail()
        {
            // Cenário
            Employee employee = ObjectMother.GetEmployee();

            // Ação
            Action executeAction = () => _service.Update(employee);

            // Verifica
            executeAction.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void EmployeeService_Update_InvalidEmptyOrNullBranch_ShouldBeFail()
        {
            // Cenário
            Employee employee = ObjectMother.GetEmployeeWithEmptyBranch();
            employee.Id = 1;

            // Ação
            Action executeAction = () => _service.Update(employee);

            // Verifica
            executeAction.Should().Throw<EmployeeEmptyOrNullBranchException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void EmployeeService_Get_ShouldBeOk()
        {
            // Cenário
            int id = 1;

            _mockRepository
                .Setup(x => x.Get(id))
                .Returns(ObjectMother.GetEmployee());

            // Ação
            Employee employee = _service.Get(id);

            // Verifica
            employee.Should().NotBeNull();
            _mockRepository.Verify(x => x.Get(id));
        }

        [Test]
        public void EmployeeService_Get_InvalidId_ShouldBeOk()
        {
            // Ação
            Action executeAction = () => _service.Get(0);

            // Verifica
            executeAction.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void EmployeeService_GetAll_ShouldBeOk()
        {
            // Cenário
            _mockRepository
                .Setup(x => x.GetAll())
                .Returns(new List<Employee>()
                        {
                            new Employee { Id = 1 },
                            new Employee { Id = 2 },
                            new Employee { Id = 3 }
                        });

            // Ação
            IEnumerable<Employee> employees = _service.GetAll();

            // Verifica
            employees.Count().Should().Equals(3);
            _mockRepository.Verify(x => x.GetAll());
        }

        [Test]
        public void EmployeeService_Delete_ShouldBeOk()
        {
            // Cenário
            Employee employee = ObjectMother.GetEmployee();
            employee.Id = 1;

            _mockRepository
                .Setup(x => x.Delete(employee));

            // Ação
            _service.Delete(employee);

            // Verifica
            _mockRepository.Verify(x => x.Delete(employee));
        }

        [Test]
        public void EmployeeService_Delete_InvalidId_ShouldBeOk()
        {
            // Cenário
            Employee employee = ObjectMother.GetEmployee();

            _mockRepository
                .Setup(x => x.Delete(employee));

            // Ação
            Action executeAction = () => _service.Delete(employee);

            // Verifica
            executeAction.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
        }
    }
}
