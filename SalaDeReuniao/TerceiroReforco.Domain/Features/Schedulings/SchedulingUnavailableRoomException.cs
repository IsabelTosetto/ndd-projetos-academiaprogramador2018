using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerceiroReforco.Domain.Exceptions;

namespace TerceiroReforco.Domain.Features.Schedulings
{
    public class SchedulingUnavailableRoomException : BusinessException
    {
        public SchedulingUnavailableRoomException() : base("A sala não está disponível.") { }
    }
}
