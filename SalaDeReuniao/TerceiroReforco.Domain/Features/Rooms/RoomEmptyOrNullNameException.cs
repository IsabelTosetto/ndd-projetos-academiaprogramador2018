using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerceiroReforco.Domain.Exceptions;

namespace TerceiroReforco.Domain.Features.Rooms
{
    public class RoomEmptyOrNullNameException : BusinessException
    {
        public RoomEmptyOrNullNameException() : base("O nome da sala está vazia.") { }
    }
}
