using FluentAssertions;
using Moq;
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

namespace SalaDeReuniao.Application.Tests.Features.Rooms
{
    [TestFixture]
    public class RoomServiceTest
    {
        private IRoomService _service;
        private Mock<IRoomRepository> _mockRepository;

        [SetUp]
        public void Initialize()
        {
            _mockRepository = new Mock<IRoomRepository>();
            _service = new RoomService(_mockRepository.Object);
        }

        [Test]
        public void RoomService_Save_ShouldBeOk()
        {
            // Cenário
            Room room = ObjectMother.GetRoom();

            _mockRepository
                .Setup(m => m.Save(room))
                .Returns(new Room { Id = 1 });

            // Ação
            Room result = _service.Add(room);

            // Verifica
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            _mockRepository.Verify(m => m.Save(room));
        }

        [Test]
        public void RoomService_Save_InvalidEmptyOrNullName_ShouldBeFail()
        {
            // Cenário
            Room room = ObjectMother.GetRoomWithEmptyName();

            // Ação
            Action executeAction = () => _service.Add(room);

            // Verifica
            executeAction.Should().Throw<RoomEmptyOrNullNameException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void RoomService_Update_ShouldBeOk()
        {
            // Cenário
            Room room = ObjectMother.GetRoom();
            room.Id = 1;

            _mockRepository
                .Setup(m => m.Update(room))
                .Returns(new Room { Id = 1 , Name = "Treinamento" });

            // Ação
            Room result = _service.Update(room);

            // Verifica
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            _mockRepository.Verify(m => m.Update(room));
        }

        [Test]
        public void RoomService_Update_InvalidNumberOfSeats_ShouldBeFail()
        {
            // Cenário
            Room room = ObjectMother.GetRoomWithInvalidNumberOfSeats();
            room.Id = 1;

            // Ação
            Action executeAction = () => _service.Update(room);

            // Verifica
            executeAction.Should().Throw<RoomInvalidNumberOfSeatsException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void RoomService_Update_InvalidId_ShouldBeFail()
        {
            // Cenário
            Room room = ObjectMother.GetRoom();

            // Ação
            Action executeAction = () => _service.Update(room);

            // Verifica
            executeAction.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void RoomService_Get_ShouldBeOk()
        {
            // Cenário
            int id = 1;

            _mockRepository
                .Setup(m => m.Get(id))
                .Returns(ObjectMother.GetRoom());

            // Ação
            Room result = _service.Get(id);

            // Verifica
            result.Should().NotBeNull();
            _mockRepository.Verify(m => m.Get(id));
        }

        [Test]
        public void RoomService_Get_InvalidId_ShouldBeFail()
        {
            // Ação
            Action executeAction = () => _service.Get(0);

            // Verifica
            executeAction.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void RoomService_GetAll_ShouldBeOk()
        {
            // Cenário
            _mockRepository
                .Setup(m => m.GetAll())
                .Returns(new List<Room>()
                        {
                            new Room { Id = 1 },
                            new Room { Id = 2 },
                            new Room { Id = 3 }
                        });

            // Ação
            IEnumerable<Room> rooms = _service.GetAll();

            // Verifica
            rooms.Count().Should().Equals(3);
            _mockRepository.Verify(m => m.GetAll());
        }

        [Test]
        public void RoomService_Delete_ShouldBeOk()
        {
            // Cenário
            Room room = ObjectMother.GetRoom();
            room.Id = 1;

            _mockRepository
                .Setup(m => m.Delete(room));

            // Ação
            _service.Delete(room);

            // Verifica
            _mockRepository.Verify(m => m.Delete(room));
        }

        [Test]
        public void RoomService_Delete_InvalidId_ShouldBeFail()
        {
            // Cenário
            Room room = ObjectMother.GetRoom();

            _mockRepository
                .Setup(m => m.Delete(room));

            // Ação
            Action executeAction = () => _service.Delete(room);

            // Verifica
            executeAction.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
        }
    }
}
