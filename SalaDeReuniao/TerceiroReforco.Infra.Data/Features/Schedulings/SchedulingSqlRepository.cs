using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerceiroReforco.Domain.Exceptions;
using TerceiroReforco.Domain.Features.Employees;
using TerceiroReforco.Domain.Features.Rooms;
using TerceiroReforco.Domain.Features.Schedulings;

namespace TerceiroReforco.Infra.Data.Features.Schedulings
{
    public class SchedulingSqlRepository : ISchedulingRepository
    {
        public Scheduling Save(Scheduling scheduling)
        {
            scheduling.Validate();

            string sql = "INSERT INTO TBScheduling(StartTime,EndTime,EmployeeId,RoomId) VALUES (@StartTime, @EndTime, @EmployeeId, @RoomId)";

            scheduling.Id = Db.Insert(sql, Take(scheduling, false));

            return scheduling;
        }

        public Scheduling Update(Scheduling scheduling)
        {
            if(scheduling.Id < 1)
                throw new IdentifierUndefinedException();

            scheduling.Validate();

            string sql = "UPDATE TBScheduling SET StartTime = @StartTime, EndTime = @EndTime, EmployeeId = @EmployeeId, RoomId = @RoomId  WHERE Id = @Id";

            Db.Update(sql, Take(scheduling));

            return scheduling;
        }

        public Scheduling Get(long id)
        {
            if (id < 1)
                throw new IdentifierUndefinedException();

            string sql = @"SELECT
                S.Id,
                S.StartTime,
                S.EndTime,
                E.Id AS EmployeeId,
			    E.Name AS EmployeeName,
                R.Id AS RoomId,
                R.Name AS RoomName
            FROM
                TBScheduling AS S
				INNER JOIN
				TBEmployee AS E ON E.Id = S.EmployeeId
                INNER JOIN
				TBRoom AS R ON R.Id = S.RoomId
            WHERE S.Id = @Id";

            return Db.Get(sql, Make, new object[] { "Id", id });
        }

        public IEnumerable<Scheduling> GetAll()
        {
            string sql = @"SELECT
                S.Id,
                S.StartTime,
                S.EndTime,
                E.Id AS EmployeeId,
			    E.Name AS EmployeeName,
                R.Id AS RoomId,
                R.Name AS RoomName
            FROM
                TBScheduling AS S
				INNER JOIN
				TBEmployee AS E ON E.Id = S.EmployeeId
                INNER JOIN
				TBRoom AS R ON R.Id = S.RoomId";

            return Db.GetAll(sql, Make);
        }

        public bool Delete(Scheduling scheduling)
        {
            if (scheduling.Id < 1)
                throw new IdentifierUndefinedException();

            string sql = "DELETE FROM TBScheduling WHERE Id = @Id";

            Db.Delete(sql, new object[] { "Id", scheduling.Id });

            return true;
        }

        public bool CheckAvailableRoom(Scheduling scheduling)
        {
            IEnumerable<Scheduling> schedulings = GetAll();

            if (CheckBusyTimeStartTime(schedulings, scheduling) && CheckBusyTimeEndTime(schedulings, scheduling))
                return true;

            return false;
        }

        private bool CheckBusyTimeStartTime(IEnumerable<Scheduling> schedulings, Scheduling scheduling)
        {
            foreach (Scheduling s in schedulings)
            {
                if (s.Room == scheduling.Room)
                {
                    if (s.StartTime.Day == scheduling.StartTime.Day)
                    {
                        if (s.StartTime.Hour == scheduling.StartTime.Hour)
                        {
                            return true; //hora ocupada
                        }
                        if (scheduling.CompareSmallerEndTime(s.StartTime))
                        { //Confere se a hora inicial é menor que a hora final de outros agendamentos
                            return true; //hora ocupada
                        }
                    }
                }
            }
            return false; //hora livre
        }

        private bool CheckBusyTimeEndTime(IEnumerable<Scheduling> schedulings, Scheduling scheduling)
        {
            foreach (Scheduling s in schedulings)
            {
                if (s.Room == scheduling.Room)
                {
                    if (s.EndTime.Day == scheduling.EndTime.Day)
                    {
                        if (scheduling.EndTime.Hour > s.StartTime.Hour)
                        {
                            return true; //hora final ocupada
                        }
                    }
                }
            }
            return false; //hora livre
        }

        /// <summary>
        /// Cria um objeto Customer baseado no DataReader.
        /// </summary>
        private static Func<IDataReader, Scheduling> Make = reader =>
           new Scheduling
           {
               Id = Convert.ToInt32(reader["Id"]),
               StartTime = Convert.ToDateTime(reader["StartTime"]),
               EndTime = Convert.ToDateTime(reader["EndTime"]),
               Employee = new Employee
               {
                   Id = Convert.ToInt32(reader["EmployeeId"]),
                   Name = reader["EmployeeName"].ToString()
               },
               Room = new Room
               {
                   Id = Convert.ToInt32(reader["RoomId"]),
                   Name = reader["RoomName"].ToString()
               }
           };

        /// <summary>
        /// Cria a lista de parametros do objeto Post para passar para o comando Sql
        /// </summary>
        /// <param name="scheduling">Post.</param>
        /// <returns>lista de parametros</returns>
        private object[] Take(Scheduling scheduling, bool hasId = true)
        {
            object[] parametros = null;

            if (hasId)
            {
                parametros = new object[]
                    {
                "@StartTime", scheduling.StartTime,
                "@EndTime", scheduling.EndTime,
                "@EmployeeId", scheduling.Employee.Id,
                "@RoomId", scheduling.Room.Id,
                "@Id", scheduling.Id
                    };
            }
            else
            {
                parametros = new object[]
              {
                "@StartTime", scheduling.StartTime,
                "@EndTime", scheduling.EndTime,
                "@EmployeeId", scheduling.Employee.Id,
                "@RoomId", scheduling.Room.Id
              };
            }
            return parametros;
        }
    }
}
