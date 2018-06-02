using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerceiroReforco.Domain.Exceptions;
using TerceiroReforco.Domain.Features.Employees;

namespace TerceiroReforco.Application.Features.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public Employee Add(Employee employee)
        {
            employee.Validate();

            return _repository.Save(employee);
        }

        public Employee Update(Employee employee)
        {
            if (employee.Id < 1)
                throw new IdentifierUndefinedException();

            employee.Validate();

            return _repository.Update(employee);
        }

        public Employee Get(long id)
        {
            if (id < 1)
                throw new IdentifierUndefinedException();

            return _repository.Get(id);
        }

        public IEnumerable<Employee> GetAll()
        {
            return _repository.GetAll();
        }

        public void Delete(Employee employee)
        {
            if (employee.Id < 1)
                throw new IdentifierUndefinedException();

            _repository.Delete(employee);
        }
    }
}
