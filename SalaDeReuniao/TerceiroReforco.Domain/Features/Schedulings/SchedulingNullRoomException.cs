using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerceiroReforco.Domain.Exceptions;

namespace TerceiroReforco.Domain.Features.Schedulings
{
    public class SchedulingNullRoomException : BusinessException
    {
        public SchedulingNullRoomException() : base("A sala não pode ser nula.") { }
    }
}
