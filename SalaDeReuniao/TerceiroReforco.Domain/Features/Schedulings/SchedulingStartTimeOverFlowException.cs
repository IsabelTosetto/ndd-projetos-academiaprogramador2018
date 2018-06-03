using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerceiroReforco.Domain.Exceptions;

namespace TerceiroReforco.Domain.Features.Schedulings
{
    public class SchedulingStartTimeOverFlowException : BusinessException
    {
        public SchedulingStartTimeOverFlowException() : base("A data inicial é maior que a atual.") { }
    }
}
