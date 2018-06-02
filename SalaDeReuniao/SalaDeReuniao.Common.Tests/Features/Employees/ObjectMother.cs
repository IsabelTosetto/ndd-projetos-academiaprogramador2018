using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerceiroReforco.Domain.Features.Employees;

namespace SalaDeReuniao.Common.Tests.Base
{
    public static partial class ObjectMother
    {
        public static Employee GetEmployee()
        {
            return new Employee()
            {
                Name = "Luciane",
                Position = "Cargo",
                Branch = "Ramal"
            };
        }

        public static Employee GetEmployeeWithEmptyName()
        {
            return new Employee()
            {
                Name = "",
                Position = "Cargo",
                Branch = "Ramal"
            };
        }

        public static Employee GetEmployeeWithEmptyPosition()
        {
            return new Employee()
            {
                Name = "Luciane",
                Position = "",
                Branch = "Ramal"
            };
        }

        public static Employee GetEmployeeWithEmptyBranch()
        {
            return new Employee()
            {
                Name = "Luciane",
                Position = "Cargo",
                Branch = ""
            };
        }
    }
}
