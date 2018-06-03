using FluentAssertions;
using NUnit.Framework;
using SalaDeReuniao.Common.Tests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerceiroReforco.Domain.Features.Employees;

namespace SalaDeReuniao.Domain.Tests.Features.Employees
{
    [TestFixture]
    public class EmployeeTest
    {
        [Test]
        public void Employee_Valid_ShouldBeOk()
        {
            //Cenário
            Employee employee = ObjectMother.GetEmployee();
            employee.Id = 0;

            //Ação
            Action comparison = employee.Validate;

            //Verifica
            comparison.Should().NotThrow<Exception>();
        }

        [Test]
        public void Employee_InvalidEmptyOrNullName_ShouldBeFail()
        {
            //Cenário
            Employee employee = ObjectMother.GetEmployeeWithEmptyName();
            employee.Id = 0;

            //Ação
            Action comparison = employee.Validate;

            //Verifica
            comparison.Should().Throw<EmployeeEmptyOrNullNameException>();
        }

        [Test]
        public void Employee_InvalidEmptyOrNullPosition_ShouldBeFail()
        {
            //Cenário
            Employee employee = ObjectMother.GetEmployeeWithEmptyPosition();
            employee.Id = 0;

            //Ação
            Action comparison = employee.Validate;

            //Verifica
            comparison.Should().Throw<EmployeeEmptyOrNullPositionException>();
        }

        [Test]
        public void Employee_InvalidEmptyOrNullBranch_ShouldBeFail()
        {
            //Cenário
            Employee employee = ObjectMother.GetEmployeeWithEmptyBranch();
            employee.Id = 0;

            //Ação
            Action comparison = employee.Validate;

            //Verifica
            comparison.Should().Throw<EmployeeEmptyOrNullBranchException>();
        }
    }
}
