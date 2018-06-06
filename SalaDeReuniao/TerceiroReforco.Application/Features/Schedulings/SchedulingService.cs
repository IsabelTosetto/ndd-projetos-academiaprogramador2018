using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerceiroReforco.Domain.Exceptions;
using TerceiroReforco.Domain.Features.Schedulings;

namespace TerceiroReforco.Application.Features.Schedulings
{
    public class SchedulingService : ISchedulingService
    {
        private ISchedulingRepository _repository;

        public SchedulingService(ISchedulingRepository repository)
        {
            _repository = repository;
        }

        public Scheduling Add(Scheduling scheduling)
        {
            scheduling.Validate();

            return _repository.Save(scheduling);
        }

        public Scheduling Update(Scheduling scheduling)
        {
            if (scheduling.Id < 1)
                throw new IdentifierUndefinedException();

            scheduling.Validate();

            return _repository.Update(scheduling);
        }

        public Scheduling Get(long id)
        {
            if (id < 1)
                throw new IdentifierUndefinedException();

            return _repository.Get(id);
        }

        public IEnumerable<Scheduling> GetAll()
        {
            return _repository.GetAll();
        }

        public void Delete(Scheduling scheduling)
        {
            if (scheduling.Id < 1)
                throw new IdentifierUndefinedException();

            _repository.Delete(scheduling);
        }

        public bool CheckAvailableRoom(Scheduling scheduling)
        {
            IEnumerable<Scheduling> schedulings = _repository.GetAll();

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
    }
}
