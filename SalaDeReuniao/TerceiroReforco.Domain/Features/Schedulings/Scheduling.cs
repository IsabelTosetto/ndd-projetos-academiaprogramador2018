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

            if (Room.Disponibility == false)
                throw new SchedulingUnavailableRoomException();
        }

        public bool CheckBusyTime(List<Scheduling> schedulings)
        {
            if (CheckBusyTimeStartTime(schedulings) && CheckBusyTimeEndTime(schedulings))
                return true;

            return false;
        }

        private bool CheckBusyTimeStartTime(List<Scheduling> schedulings)
        {
            foreach (Scheduling s in schedulings)
            {
                if (s.Room == Room)
                {
                    if (s.StartTime.Day == StartTime.Day)
                    {
                        if (s.StartTime.Hour == StartTime.Hour)
                        {
                            return true; //hora ocupada
                        }
                        if (CompareSmallerEndTime(s.StartTime))
                        { //Confere se a hora inicial é menor que a hora final de outros agendamentos
                            return true; //hora ocupada
                        }
                    }
                }
            }
            return false; //hora livre
        }

        private bool CheckBusyTimeEndTime(List<Scheduling> schedulings)
        {
            foreach (Scheduling s in schedulings)
            {
                if (s.Room == Room)
                {
                    if (s.EndTime.Day == EndTime.Day)
                    {
                        if (EndTime.Hour > s.StartTime.Hour)
                        {
                            return true; //hora final ocupada
                        }
                    }
                }
            }
            return false; //hora livre
        }

        private bool CompareStartTimeSmallerCurrent(DateTime dt)
        {
            int result = DateTime.Compare(dt, DateTime.Now);
            if (result <= 0)
            {
                return true;
            }
            return false;
        }

        private bool CompareEndTimeBiggerStartTime()
        {
            int result = DateTime.Compare(EndTime, StartTime);
            if (result <= 0)
            {
                return true;
            }
            return false;
        }

        private bool CompareSmallerEndTime(DateTime dt)
        {
            if (dt.Hour < EndTime.Hour)
            {
                return true; //menor que a hora final
            }
            return false;
        }
    }
}
