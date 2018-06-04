using FluentAssertions;
using NUnit.Framework;
using SalaDeReuniao.Common.Tests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerceiroReforco.Application.Features.Schedulings;
using TerceiroReforco.Domain.Exceptions;
using TerceiroReforco.Domain.Features.Employees;
using TerceiroReforco.Domain.Features.Rooms;
using TerceiroReforco.Domain.Features.Schedulings;
using TerceiroReforco.Infra.Data.Features.Schedulings;

namespace SalaDeReuniao.Integration.Test.Features.Schedulings
{
    [TestFixture]
    public class SchedulingIntegrationTest
    {
        private ISchedulingService _service;
        private ISchedulingRepository _repository;
        private Employee _employee;
        private Room _room;

        [SetUp]
        public void Initialize()
        {
            BaseSqlTest.SeedDatabase();
            _repository = new SchedulingSqlRepository();
            _service = new SchedulingService(_repository);
            _employee = ObjectMother.GetEmployee();
            _employee.Id = 1;
            _room = ObjectMother.GetRoom();
            _room.Id = 1;
        }

        [Order(1)]
        [Test]
        public void SchedulingIntegration_Add_ShouldBeOk()
        {
            // Cenário
            Scheduling scheduling = ObjectMother.GetScheduling();
            scheduling.Employee = _employee;
            scheduling.Room = _room;

            // Ação
            Scheduling result = _service.Add(scheduling);

            // Verifica
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
        }

        [Order(2)]
        [Test]
        public void SchedulingIntegration_Add_InvalidEmptyOrNullName_ShouldBeFail()
        {
            // Cenário
            Scheduling scheduling = ObjectMother.GetSchedulingInvalidStartTime();

            // Ação
            Action executeAction = () => _service.Add(scheduling);

            // Verifica
            executeAction.Should().Throw<SchedulingStartTimeOverFlowException>();
        }

        [Order(3)]
        [Test]
        public void SchedulingIntegration_Update_ShouldBeOk()
        {
            // Cenário
            Scheduling scheduling = _service.Get(1);
            DateTime oldStartTime = scheduling.StartTime;
            scheduling.StartTime = new DateTime(2018, 6, 10, 7, 0, 0);
            scheduling.Room.Disponibility = true;

            // Ação
            Scheduling result = _service.Update(scheduling);

            // Verifica
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            result.StartTime.Should().NotBe(oldStartTime);
        }

        [Order(4)]
        [Test]
        public void SchedulingIntegration_Update_InvalidId_ShouldBeFail()
        {
            // Cenário
            Scheduling scheduling = ObjectMother.GetScheduling();

            // Ação
            Action executeAction = () => _service.Update(scheduling);

            // Verifica
            executeAction.Should().Throw<IdentifierUndefinedException>();
        }

        [Order(5)]
        [Test]
        public void SchedulingIntegration_Get_ShouldBeOk()
        {
            // Cenário
            int id = 1;

            // Ação
            Scheduling scheduling = _service.Get(id);

            // Verifica
            scheduling.Should().NotBeNull();
        }

        [Order(6)]
        [Test]
        public void SchedulingIntegration_Get_InvalidId_ShouldBeOk()
        {
            // Ação
            Action executeAction = () => _service.Get(0);

            // Verifica
            executeAction.Should().Throw<IdentifierUndefinedException>();
        }

        [Order(7)]
        [Test]
        public void SchedulingIntegration_GetAll_ShouldBeOk()
        {
            // Ação
            IEnumerable<Scheduling> schedulings = _service.GetAll();

            // Verifica
            schedulings.Count().Should().Equals(1);
        }

        [Order(8)]
        [Test]
        public void SchedulingIntegration_Delete_ShouldBeOk()
        {
            // Cenário
            Scheduling scheduling = _service.Get(1);

            // Ação
            _service.Delete(scheduling);

            // Verifica
            Scheduling result = _service.Get(1);
            result.Should().BeNull();
        }

        [Order(9)]
        [Test]
        public void SchedulingIntegration_Delete_InvalidId_ShouldBeOk()
        {
            // Cenário
            Scheduling scheduling = ObjectMother.GetScheduling();

            // Ação
            Action executeAction = () => _service.Delete(scheduling);

            // Verifica
            executeAction.Should().Throw<IdentifierUndefinedException>();
        }
    }
}
