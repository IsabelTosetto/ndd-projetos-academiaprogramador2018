using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerceiroReforco.Domain.Features.Employees;
using TerceiroReforco.Domain.Features.Rooms;

namespace TerceiroReforco.Domain.Features.Schedulings
{
    public class Scheduling
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Employee Employee { get; set; }
        public Room Room { get; set; }

        public void Validate()
        {
            if (CompareStartTimeSmallerCurrent(StartTime))
                throw new SchedulingStartTimeOverFlowException();

            if (CompareEndTimeBiggerStartTime())
                throw new SchedulingEndTimeLessThanStartTimeException();

            if (Employee == null)
                throw new SchedulingNullEmployeeException();

            if (Room == null)
                throw new SchedulingNullRoomException();
        }

        public bool CompareStartTimeSmallerCurrent(DateTime dt)
        {
            int result = DateTime.Compare(dt, DateTime.Now);
            if (result <= 0)
            {
                return true;
            }
            return false;
        }

        public bool CompareEndTimeBiggerStartTime()
        {
            int result = DateTime.Compare(EndTime, StartTime);
            if (result <= 0)
            {
                return true;
            }
            return false;
        }
    }
}
