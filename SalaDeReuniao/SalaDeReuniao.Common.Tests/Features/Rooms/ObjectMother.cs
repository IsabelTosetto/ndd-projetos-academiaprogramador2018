using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerceiroReforco.Domain.Features.Rooms;

namespace SalaDeReuniao.Common.Tests.Base
{
    public static partial class ObjectMother
    {
        public static Room GetRoom()
        {
            return new Room()
            {
                Name = "Treinamento",
                NumberOfSeats = 30
            };
        }

        public static Room GetRoomWithEmptyName()
        {
            return new Room()
            {
                Name = "",
                NumberOfSeats = 30
            };
        }

        public static Room GetRoomWithInvalidNumberOfSeats()
        {
            return new Room()
            {
                Name = "Treinamento",
                NumberOfSeats = 0
            };
        }
    }
}
