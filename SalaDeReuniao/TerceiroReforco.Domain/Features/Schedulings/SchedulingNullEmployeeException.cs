using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerceiroReforco.Domain.Exceptions;

namespace TerceiroReforco.Domain.Features.Schedulings
{
    public class SchedulingNullEmployeeException : BusinessException
    {
        public SchedulingNullEmployeeException() : base("O funcionário não pode ser nulo.") { }
    }
}
