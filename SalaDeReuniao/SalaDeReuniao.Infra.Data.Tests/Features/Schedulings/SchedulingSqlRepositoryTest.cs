using FluentAssertions;
using Moq;
using NUnit.Framework;
using SalaDeReuniao.Common.Tests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerceiroReforco.Domain.Exceptions;
using TerceiroReforco.Domain.Features.Employees;
using TerceiroReforco.Domain.Features.Rooms;
using TerceiroReforco.Domain.Features.Schedulings;
using TerceiroReforco.Infra.Data.Features.Schedulings;

namespace SalaDeReuniao.Infra.Data.Tests.Features.Schedulings
{
    [TestFixture]
    public class SchedulingSqlRepositoryTest
    {
        private ISchedulingRepository _repository;
        private Mock<Employee> _mockEmployee;
        private Mock<Room> _mockRoom;

        [SetUp]
        public void Initialize()
        {
            BaseSqlTest.SeedDatabase();
            _repository = new SchedulingSqlRepository();

            _mockEmployee = new Mock<Employee>();
            _mockRoom = new Mock<Room>();
        }

        [Test]
        public void SchedulingSqlRepository_Save_ShouldBeOk()
        {
            //Cenário
            Scheduling scheduling = ObjectMother.GetScheduling();
            scheduling.Employee = _mockEmployee.Object;
            _mockEmployee.Object.Id = 1;
            scheduling.Room = _mockRoom.Object;
            _mockRoom.Object.Id = 1;
            _mockRoom.Object.Disponibility = true;

            //Ação
            Scheduling result = _repository.Save(scheduling);

            //Verifica
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void SchedulingSqlRepository_Save_InvalidNullRoom_ShouldBeFail()
        {
            //Cenário
            Scheduling scheduling = ObjectMother.GetScheduling();
            scheduling.Employee = _mockEmployee.Object;
            _mockEmployee.Object.Id = 1;

            //Ação
            Action comparison = () => _repository.Save(scheduling);

            //Verifica
            comparison.Should().Throw<SchedulingNullRoomException>();
        }

        [Test]
        public void SchedulingSqlRepository_Update_ShouldBeOk()
        {
            //Cenário
            Scheduling scheduling = _repository.Get(1);
            DateTime oldStartTime = scheduling.StartTime;
            scheduling.StartTime = new DateTime(2018, 6, 10, 7, 0, 0);
            scheduling.Room.Disponibility = true;

            //Ação
            Scheduling result = _repository.Update(scheduling);

            //Verifica
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            result.StartTime.Should().NotBe(oldStartTime);
        }

        [Test]
        public void SchedulingSqlRepository_Update_InvalidId_ShouldBeFail()
        {
            //Cenário
            Scheduling scheduling = ObjectMother.GetScheduling();

            //Ação
            Action comparison = () => _repository.Update(scheduling);

            //Verifica
            comparison.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void SchedulingSqlRepository_Get_ShouldBeOk()
        {
            //Ação
            Scheduling result = _repository.Get(1);

            //Verifica
            result.Should().NotBeNull();
        }

        [Test]
        public void SchedulingSqlRepository_Get_InvalidId_ShouldBeFail()
        {
            //Ação
            Action comparison = () => _repository.Get(0);

            //Verifica
            comparison.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void SchedulingSqlRepository_GetAll_ShouldBeOk()
        {
            //Ação
            var result = _repository.GetAll();

            //Verifica
            result.Should().NotBeNull();
        }

        [Test]
        public void SchedulingSqlRepository_Delete_ShouldBeOk()
        {
            //Cenário
            Scheduling scheduling = _repository.Get(1);

            //Ação
            _repository.Delete(scheduling);

            //Verifica
            Scheduling result = _repository.Get(1);
            result.Should().BeNull();
        }

        [Test]
        public void SchedulingSqlRepository_Delete_InvalidId_ShouldBeFail()
        {
            //Cenário
            Scheduling scheduling = ObjectMother.GetScheduling();

            //Ação
            Action comparison = () => _repository.Delete(scheduling);

            //Verifica
            comparison.Should().Throw<IdentifierUndefinedException>();
        }
    }
}
