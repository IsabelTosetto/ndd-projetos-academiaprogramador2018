using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerceiroReforco.Domain.Exceptions;
using TerceiroReforco.Domain.Features.Employees;

namespace TerceiroReforco.Infra.Data.Features.Employees
{
    public class EmployeeSqlRepository : IEmployeeRepository
    {
        public Employee Save(Employee employee)
        {
            employee.Validate();

            string sql = "INSERT INTO TBEmployee(Name,Position,Branch) " +
            "VALUES (@Name, @Position, @Branch)";

            employee.Id = Db.Insert(sql, Take(employee, false));

            return employee;
        }

        public Employee Update(Employee employee)
        {
            if(employee.Id < 1)
                throw new IdentifierUndefinedException();

            employee.Validate();

            string sql = "UPDATE TBEmployee SET Name = @Name, Position = @Position, Branch = @Branch WHERE Id = @Id";

            Db.Update(sql, Take(employee));

            return employee;
        }

        public Employee Get(long id)
        {
            if (id < 1)
                throw new IdentifierUndefinedException();

            string sql = "SELECT * FROM TBEmployee WHERE Id = @Id";

            return Db.Get(sql, Make, new object[] { "Id", id });
        }

        public IEnumerable<Employee> GetAll()
        {
            string sql = "SELECT * FROM TBEmployee";

            return Db.GetAll(sql, Make);
        }

        public bool Delete(Employee employee)
        {
            if (employee.Id < 1)
                throw new IdentifierUndefinedException();

            string sql = "DELETE FROM TBEmployee WHERE Id = @Id";

            Db.Delete(sql, new object[] { "Id", employee.Id });
            return true;
        }

        /// <summary>
        /// Cria um objeto Customer baseado no DataReader.
        /// </summary>
        private static Func<IDataReader, Employee> Make = reader =>
           new Employee
           {
               Id = Convert.ToInt32(reader["Id"]),
               Name = reader["Name"].ToString(),
               Position = reader["Position"].ToString(),
               Branch = reader["Branch"].ToString()
           };

        /// <summary>
        /// Cria a lista de parametros do objeto Post para passar para o comando Sql
        /// </summary>
        /// <param name="employee">Post.</param>
        /// <returns>lista de parametros</returns>
        private object[] Take(Employee employee, bool hasId = true)
        {
            object[] parametros = null;

            if (hasId)
            {
                parametros = new object[]
                    {
                "@Name", employee.Name,
                "@Position", employee.Position,
                "@Branch", employee.Branch,
                "@Id", employee.Id,
                    };
            }
            else
            {
                parametros = new object[]
              {
                "@Name", employee.Name,
                "@Position", employee.Position,
                "@Branch", employee.Branch
              };
            }
            return parametros;
        }
    }
}
