using FluentAssertions;
using NUnit.Framework;
using SalaDeReuniao.Common.Tests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerceiroReforco.Domain.Exceptions;
using TerceiroReforco.Domain.Features.Employees;
using TerceiroReforco.Infra.Data.Features.Employees;

namespace SalaDeReuniao.Infra.Data.Tests.Features.Employees
{
    [TestFixture]
    public class EmployeeSqlRepositoryTest
    {
        private IEmployeeRepository _repository;

        [SetUp]
        public void Initialize()
        {
            BaseSqlTest.SeedDatabase();
            _repository = new EmployeeSqlRepository();
        }

        [Test]
        public void EmployeeSqlRepository_Save_ShouldBeOk()
        {
            //Cenário
            Employee employee = ObjectMother.GetEmployee();

            //Ação
            Employee result = _repository.Save(employee);

            //Verifica
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void EmployeeSqlRepository_Save_InvalidNullOrEmptyName_ShouldBeFail()
        {
            //Cenário
            Employee employee = ObjectMother.GetEmployeeWithEmptyName();

            //Ação
            Action executeAction = () => _repository.Save(employee);

            //Verifica
            executeAction.Should().Throw<EmployeeEmptyOrNullNameException>();
        }

        [Test]
        public void EmployeeSqlRepository_Update_ShouldBeOk()
        {
            //Cenário
            Employee employee = _repository.Get(1);
            string oldName = employee.Name;
            employee.Name = "Liana";

            //Ação
            Employee result = _repository.Update(employee);

            //Verifica
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            result.Name.Should().NotBe(oldName);
        }

        [Test]
        public void EmployeeSqlRepository_Update_InvalidNullOrEmptyName_ShouldBeFail()
        {
            //Cenário
            Employee employee = ObjectMother.GetEmployeeWithEmptyName();
            employee.Id = 1;

            //Ação
            Action executeAction = () => _repository.Update(employee);

            //Verifica
            executeAction.Should().Throw<EmployeeEmptyOrNullNameException>();
        }

        [Test]
        public void EmployeeSqlRepository_Update_InvalidId_ShouldBeFail()
        {
            //Cenário
            Employee employee = ObjectMother.GetEmployee();

            //Ação
            Action executeAction = () => _repository.Update(employee);

            //Verifica
            executeAction.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void EmployeeSqlRepository_Get_ShouldBeOk()
        {
            //Ação
            Employee result = _repository.Get(1);

            //Verifica
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
        }

        [Test]
        public void EmployeeSqlRepository_Get_InvalidId_ShouldBeFail()
        {
            //Ação
            Action executeAction = () => _repository.Get(0);

            //Verifica
            executeAction.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void EmployeeSqlRepository_GetAll_ShouldBeOk()
        {
            //Ação
            IEnumerable<Employee> employees = _repository.GetAll();

            //Verifica
            employees.Count().Should().BeGreaterThan(0);
        }

        [Test]
        public void EmployeeSqlRepository_Delete_ShouldBeOk()
        {
            //Cenário
            Employee employee = _repository.Get(1);

            //Ação
            _repository.Delete(employee);

            //Verifica
            Employee result = _repository.Get(1);
            result.Should().BeNull();
        }

        [Test]
        public void EmployeeSqlRepository_Delete_InvalidId_ShouldBeFail()
        {
            //Cenário
            Employee employee = ObjectMother.GetEmployee();

            //Ação
            Action executeAction = () => _repository.Delete(employee);

            //Verifica
            executeAction.Should().Throw<IdentifierUndefinedException>();
        }
    }
}
