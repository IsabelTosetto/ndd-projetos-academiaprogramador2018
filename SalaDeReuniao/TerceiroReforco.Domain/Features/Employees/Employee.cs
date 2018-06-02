using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerceiroReforco.Domain.Features.Employees
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; } //Cargo
        public string Branch { get; set; } //Ramal

        public void Validate()
        {
            if (string.IsNullOrEmpty(Name))
                throw new EmployeeEmptyOrNullNameException();

            if (string.IsNullOrEmpty(Position))
                throw new EmployeeEmptyOrNullPositionException();

            if (string.IsNullOrEmpty(Branch))
                throw new EmployeeEmptyOrNullBranchException();
        }
    }
}
