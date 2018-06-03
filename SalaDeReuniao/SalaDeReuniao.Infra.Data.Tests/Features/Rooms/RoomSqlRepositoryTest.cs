using FluentAssertions;
using NUnit.Framework;
using SalaDeReuniao.Common.Tests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerceiroReforco.Domain.Exceptions;
using TerceiroReforco.Domain.Features.Rooms;
using TerceiroReforco.Infra.Data.Features.Rooms;

namespace SalaDeReuniao.Infra.Data.Tests.Features.Rooms
{
    [TestFixture]
    public class RoomSqlRepositoryTest
    {
        private IRoomRepository _repository;

        [SetUp]
        public void Initialize()
        {
            BaseSqlTest.SeedDatabase();
            _repository = new RoomSqlRepository();
        }

        [Test]
        public void RoomSqlRepository_Save_ShouldBeOk()
        {
            //Cenário
            Room room = ObjectMother.GetRoom();

            //Ação
            Room result = _repository.Save(room);

            //Verifica
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void RoomSqlRepository_Save_InvalidNullOrEmptyName_ShouldBeFail()
        {
            //Cenário
            Room room = ObjectMother.GetRoomWithEmptyName();

            //Ação
            Action executeAction = () => _repository.Save(room);

            //Verifica
            executeAction.Should().Throw<RoomEmptyOrNullNameException>();
        }

        [Test]
        public void RoomSqlRepository_Update_ShouldBeOk()
        {
            //Cenário
            Room room = _repository.Get(1);
            string oldName = room.Name;
            room.Name = "Entrevistas";

            //Ação
            Room result = _repository.Update(room);

            //Verifica
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            result.Name.Should().NotBe(oldName);
        }

        [Test]
        public void RoomSqlRepository_Update_InvalidNumberOfSeats_ShouldBeFail()
        {
            //Cenário
            Room room = ObjectMother.GetRoomWithInvalidNumberOfSeats();
            room.Id = 1;

            //Ação
            Action executeAction = () => _repository.Update(room);

            //Verifica
            executeAction.Should().Throw<RoomInvalidNumberOfSeatsException>();
        }

        [Test]
        public void RoomSqlRepository_Update_InvalidId_ShouldBeFail()
        {
            //Cenário
            Room room = ObjectMother.GetRoom();

            //Ação
            Action executeAction = () => _repository.Update(room);

            //Verifica
            executeAction.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void RoomSqlRepository_Get_ShouldBeOk()
        {
            //Ação
            Room result = _repository.Get(1);

            //Verifica
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
        }

        [Test]
        public void RoomSqlRepository_Get_InvalidId_ShouldBeFail()
        {
            //Ação
            Action executeAction = () => _repository.Get(0);

            //Verifica
            executeAction.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void RoomSqlRepository_GetAll_ShouldBeOk()
        {
            //Ação
            IEnumerable<Room> rooms = _repository.GetAll();

            //Verifica
            rooms.Count().Should().BeGreaterThan(0);
        }

        [Test]
        public void RoomSqlRepository_Delete_ShouldBeOk()
        {
            //Cenário
            Room room = _repository.Get(1);

            //Ação
            _repository.Delete(room);

            //Verifica
            Room result = _repository.Get(1);
            result.Should().BeNull();
        }

        [Test]
        public void RoomSqlRepository_Delete_InvalidId_ShouldBeOk()
        {
            //Cenário
            Room room = ObjectMother.GetRoom();

            //Ação
            Action executeAction = () => _repository.Delete(room);

            //Verifica
            executeAction.Should().Throw<IdentifierUndefinedException>();
        }
    }
}
