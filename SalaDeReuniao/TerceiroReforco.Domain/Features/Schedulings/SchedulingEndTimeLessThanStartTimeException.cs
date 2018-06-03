using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerceiroReforco.Domain.Exceptions;

namespace TerceiroReforco.Domain.Features.Schedulings
{
    public class SchedulingEndTimeLessThanStartTimeException : BusinessException
    {
        public SchedulingEndTimeLessThanStartTimeException() : base("A data final é menor que a data inicial.") { }
    }
}
