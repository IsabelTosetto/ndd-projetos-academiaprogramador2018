using FluentAssertions;
using NUnit.Framework;
using SalaDeReuniao.Common.Tests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerceiroReforco.Domain.Features.Rooms;

namespace SalaDeReuniao.Domain.Tests.Features.Rooms
{
    [TestFixture]
    public class RoomTest
    {
        [Test]
        public void Test_Room_Valid_ShouldBeOk()
        {
            //Cenário
            Room room = ObjectMother.GetRoom();
            room.Id = 0;

            //Ação
            Action comparison = room.Validate;

            //Verifica
            comparison.Should().NotThrow<Exception>();
        }

        [Test]
        public void Test_Room_InvalidEmptyOrNullName_ShouldBeFail()
        {
            //Cenário
            Room room = ObjectMother.GetRoomWithEmptyName();
            room.Id = 0;

            //Ação
            Action comparison = room.Validate;

            //Verifica
            comparison.Should().Throw<RoomEmptyOrNullNameException>();
        }

        [Test]
        public void Test_Room_InvalidNumberOfSeats_ShouldBeFail()
        {
            //Cenário
            Room room = ObjectMother.GetRoomWithInvalidNumberOfSeats();
            room.Id = 0;

            //Ação
            Action comparison = room.Validate;

            //Verifica
            comparison.Should().Throw<RoomInvalidNumberOfSeatsException>();
        }
    }
}
