using FluentAssertions;
using Moq;
using NUnit.Framework;
using SalaDeReuniao.Common.Tests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerceiroReforco.Domain.Features.Employees;
using TerceiroReforco.Domain.Features.Rooms;
using TerceiroReforco.Domain.Features.Schedulings;

namespace SalaDeReuniao.Domain.Tests.Features.Schedulings
{
    [TestFixture]
    public class SchedulingTest
    {
        private Mock<Employee> _mockEmployee;
        private Mock<Room> _mockRoom;
        
        [SetUp]
        public void Initialize()
        {
            _mockEmployee = new Mock<Employee>();
            _mockRoom = new Mock<Room>();
        }

        [Test]
        public void Scheduling_Valid_ShouldBeOk()
        {
            //Cenário
            Scheduling scheduling = ObjectMother.GetScheduling();
            scheduling.Id = 0;
            scheduling.Employee = _mockEmployee.Object;
            scheduling.Room = _mockRoom.Object;

            //Ação
            Action comparison = scheduling.Validate;

            //Verifica
            comparison.Should().NotThrow<Exception>();
        }

        [Test]
        public void Scheduling_InvalidStartTimeOverFlow_ShouldBeFail()
        {
            //Cenário
            Scheduling scheduling = ObjectMother.GetSchedulingInvalidStartTime();
            scheduling.Id = 0;
            scheduling.Employee = _mockEmployee.Object;
            scheduling.Room = _mockRoom.Object;

            //Ação
            Action comparison = scheduling.Validate;

            //Verifica
            comparison.Should().Throw<SchedulingStartTimeOverFlowException>();
        }

        [Test]
        public void Scheduling_InvalidEndTimeLessThanStartTime_ShouldBeFail()
        {
            //Cenário
            Scheduling scheduling = ObjectMother.GetSchedulingInvalidEndTime();
            scheduling.Id = 0;
            scheduling.Employee = _mockEmployee.Object;
            scheduling.Room = _mockRoom.Object;

            //Ação
            Action comparison = scheduling.Validate;

            //Verifica
            comparison.Should().Throw<SchedulingEndTimeLessThanStartTimeException>();
        }

        [Test]
        public void Scheduling_InvalidNullEmployee_ShouldBeFail()
        {
            //Cenário
            Scheduling scheduling = ObjectMother.GetScheduling();
            scheduling.Id = 0;
            scheduling.Room = _mockRoom.Object;

            //Ação
            Action comparison = scheduling.Validate;

            //Verifica
            comparison.Should().Throw<SchedulingNullEmployeeException>();
        }

        [Test]
        public void Scheduling_InvalidNullRoom_ShouldBeFail()
        {
            //Cenário
            Scheduling scheduling = ObjectMother.GetScheduling();
            scheduling.Id = 0;
            scheduling.Employee = _mockEmployee.Object;

            //Ação
            Action comparison = scheduling.Validate;

            //Verifica
            comparison.Should().Throw<SchedulingNullRoomException>();
        }

        [Test]
        public void Scheduling_CheckBusyTimeStartTime_ShouldBeOk()
        {
            //Cenário
            Scheduling scheduling = ObjectMother.GetScheduling();

            //Ação
        }
    }
}
