using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerceiroReforco.Domain.Exceptions;

namespace TerceiroReforco.Domain.Features.Employees
{
    public class EmployeeEmptyOrNullPositionException : BusinessException
    {
        public EmployeeEmptyOrNullPositionException() : base("O cargo do funcionário está vazio.") { }
    }
}
