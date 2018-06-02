using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerceiroReforco.Domain.Exceptions;

namespace TerceiroReforco.Domain.Features.Employees
{
    public class EmployeeEmptyOrNullNameException : BusinessException
    {
        public EmployeeEmptyOrNullNameException() : base("O nome do funcionário está vazio.") { }
    }
}
