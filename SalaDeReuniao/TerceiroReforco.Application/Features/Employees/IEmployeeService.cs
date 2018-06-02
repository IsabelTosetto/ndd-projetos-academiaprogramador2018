using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerceiroReforco.Domain.Features.Employees;

namespace TerceiroReforco.Application.Features.Employees
{
    public interface IEmployeeService
    {
        Employee Add(Employee employee);
        Employee Update(Employee employee);
        Employee Get(long id);
        IEnumerable<Employee> GetAll();
        void Delete(Employee employee);
    }
}
