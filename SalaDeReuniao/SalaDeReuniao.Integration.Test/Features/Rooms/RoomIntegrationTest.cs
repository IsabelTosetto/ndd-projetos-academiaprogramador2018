using FluentAssertions;
using NUnit.Framework;
using SalaDeReuniao.Common.Tests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerceiroReforco.Application.Features.Rooms;
using TerceiroReforco.Domain.Exceptions;
using TerceiroReforco.Domain.Features.Rooms;
using TerceiroReforco.Infra.Data.Features.Rooms;

namespace SalaDeReuniao.Integration.Test.Features.Rooms
{
    [TestFixture]
    public class RoomIntegrationTest
    {
        private IRoomService _service;
        private IRoomRepository _repository;

        [SetUp]
        public void Initialize()
        {
            BaseSqlTest.SeedDatabase();
            _repository = new RoomSqlRepository();
            _service = new RoomService(_repository);
        }

        [Order(1)]
        [Test]
        public void RoomIntegration_Add_ShouldBeOk()
        {
            // Cenário
            Room room = ObjectMother.GetRoom();

            // Ação
            Room result = _service.Add(room);

            // Verifica
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
        }

        [Order(2)]
        [Test]
        public void RoomIntegration_Add_InvalidEmptyOrNullName_ShouldBeFail()
        {
            // Cenário
            Room room = ObjectMother.GetRoomWithEmptyName();

            // Ação
            Action executeAction = () => _service.Add(room);

            // Verifica
            executeAction.Should().Throw<RoomEmptyOrNullNameException>();
        }

        [Order(3)]
        [Test]
        public void RoomIntegration_Update_ShouldBeOk()
        {
            // Cenário
            Room room = _service.Get(1);
            string oldName = room.Name;
            room.Name = "Novo";

            // Ação
            Room result = _service.Update(room);

            // Verifica
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            result.Name.Should().NotBe(oldName);
        }

        [Order(4)]
        [Test]
        public void RoomIntegration_Update_InvalidId_ShouldBeFail()
        {
            // Cenário
            Room room = ObjectMother.GetRoom();

            // Ação
            Action executeAction = () => _service.Update(room);

            // Verifica
            executeAction.Should().Throw<IdentifierUndefinedException>();
        }

        [Order(5)]
        [Test]
        public void RoomIntegration_Get_ShouldBeOk()
        {
            // Cenário
            int id = 1;

            // Ação
            Room room = _service.Get(id);

            // Verifica
            room.Should().NotBeNull();
        }

        [Order(6)]
        [Test]
        public void RoomIntegration_Get_InvalidId_ShouldBeOk()
        {
            // Ação
            Action executeAction = () => _service.Get(0);

            // Verifica
            executeAction.Should().Throw<IdentifierUndefinedException>();
        }

        [Order(7)]
        [Test]
        public void RoomIntegration_GetAll_ShouldBeOk()
        {
            // Ação
            IEnumerable<Room> rooms = _service.GetAll();

            // Verifica
            rooms.Count().Should().Equals(1);
        }

        [Order(8)]
        [Test]
        public void RoomIntegration_Delete_ShouldBeOk()
        {
            // Cenário
            Room room = _service.Get(1);

            // Ação
            _service.Delete(room);

            // Verifica
            Room result = _service.Get(1);
            result.Should().BeNull();
        }

        [Order(9)]
        [Test]
        public void RoomIntegration_Delete_InvalidId_ShouldBeOk()
        {
            // Cenário
            Room room = ObjectMother.GetRoom();

            // Ação
            Action executeAction = () => _service.Delete(room);

            // Verifica
            executeAction.Should().Throw<IdentifierUndefinedException>();
        }
    }
}
