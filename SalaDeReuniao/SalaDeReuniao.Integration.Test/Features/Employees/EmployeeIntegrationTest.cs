using FluentAssertions;
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
using TerceiroReforco.Infra.Data.Features.Employees;

namespace SalaDeReuniao.Integration.Test.Features.Employees
{
    [TestFixture]
    public class EmployeeIntegrationTest
    {
        private IEmployeeService _service;
        private IEmployeeRepository _repository;

        [SetUp]
        public void Initialize()
        {
            _repository = new EmployeeSqlRepository();
            _service = new EmployeeService(_repository);
        }

        [Order(1)]
        [Test]
        public void EmployeeIntegration_Add_ShouldBeOk()
        {
            // Cenário
            Employee employee = ObjectMother.GetEmployee();

            // Ação
            Employee result = _service.Add(employee);

            // Verifica
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
        }

        [Order(2)]
        [Test]
        public void EmployeeIntegration_Add_InvalidEmptyOrNullName_ShouldBeFail()
        {
            // Cenário
            Employee employee = ObjectMother.GetEmployeeWithEmptyName();

            // Ação
            Action executeAction = () => _service.Add(employee);

            // Verifica
            executeAction.Should().Throw<EmployeeEmptyOrNullNameException>();
        }
        
        [Order(3)]
        [Test]
        public void EmployeeIntegration_Update_ShouldBeOk()
        {
            // Cenário
            Employee employee = _service.Get(1);
            string oldName = employee.Name;
            employee.Name = "Novo";

            // Ação
            Employee result = _service.Update(employee);

            // Verifica
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            result.Name.Should().NotBe(oldName);
        }

        [Order(4)]
        [Test]
        public void EmployeeIntegration_Update_InvalidId_ShouldBeFail()
        {
            // Cenário
            Employee employee = ObjectMother.GetEmployee();

            // Ação
            Action executeAction = () => _service.Update(employee);

            // Verifica
            executeAction.Should().Throw<IdentifierUndefinedException>();
        }

        [Order(5)]
        [Test]
        public void EmployeeIntegration_Get_ShouldBeOk()
        {
            // Cenário
            int id = 1;

            // Ação
            Employee employee = _service.Get(id);

            // Verifica
            employee.Should().NotBeNull();
        }

        [Order(6)]
        [Test]
        public void EmployeeIntegration_Get_InvalidId_ShouldBeOk()
        {
            // Ação
            Action executeAction = () => _service.Get(0);

            // Verifica
            executeAction.Should().Throw<IdentifierUndefinedException>();
        }

        [Order(7)]
        [Test]
        public void EmployeeIntegration_GetAll_ShouldBeOk()
        {
            // Ação
            IEnumerable<Employee> employees = _service.GetAll();

            // Verifica
            employees.Count().Should().Equals(1);
        }

        [Order(8)]
        [Test]
        public void EmployeeIntegration_Delete_ShouldBeOk()
        {
            // Cenário
            Employee employee = _service.Get(1);

            // Ação
            _service.Delete(employee);

            // Verifica
            Employee result = _service.Get(1);
            result.Should().BeNull();
        }

        [Order(9)]
        [Test]
        public void EmployeeIntegration_Delete_InvalidId_ShouldBeOk()
        {
            // Cenário
            Employee employee = ObjectMother.GetEmployee();

            // Ação
            Action executeAction = () => _service.Delete(employee);

            // Verifica
            executeAction.Should().Throw<IdentifierUndefinedException>();
        }
    }
}
