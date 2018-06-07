using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerceiroReforco.Infra;

namespace SalaDeReuniao.Common.Tests.Base
{
    public static class BaseSqlTest
    {
        private const string RECREATE_TABLES =
             "TRUNCATE TABLE [dbo].[TBScheduling]; " +
             "DELETE FROM TBEmployee DBCC CHECKIDENT('TBEmployee',RESEED,0);" +
            "DELETE FROM TBRoom DBCC CHECKIDENT('TBRoom',RESEED,0);";

        private const string INSERT_EMPLOYEE = "INSERT INTO TBEmployee(Name,Position,Branch) " +
            "VALUES ('Luciane', 'Cargo', 'Ramal')";
        private const string INSERT_ROOM = "INSERT INTO TBRoom (Name,NumberOfSeats) VALUES ('Treinamento', 30)";
        private const string INSERT_SCHEDULING = "INSERT INTO TBScheduling(StartTime,EndTime,EmployeeId,RoomId) VALUES ('2018-10-23 20:00:00', '2018-10-23 22:00:00', 1, 1)";
        
        public static void SeedDatabase()
        {
            Db.Update(RECREATE_TABLES);
            Db.Update(INSERT_EMPLOYEE);
            Db.Update(INSERT_ROOM);
            Db.Update(INSERT_SCHEDULING);
        }
    }
}
