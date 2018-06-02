using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerceiroReforco.Domain.Exceptions;

namespace TerceiroReforco.Domain.Features.Rooms
{
    public class RoomInvalidNumberOfSeatsException : BusinessException
    {
        public RoomInvalidNumberOfSeatsException() : base("O número de lugares deve ser maior de 1.") { }
    }
}
