using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerceiroReforco.Domain.Exceptions;

namespace TerceiroReforco.Domain.Features.Employees
{
    public class EmployeeEmptyOrNullBranchException : BusinessException
    {
        public EmployeeEmptyOrNullBranchException() : base("O ramal do funcionário está vazio.") { }
    }
}
