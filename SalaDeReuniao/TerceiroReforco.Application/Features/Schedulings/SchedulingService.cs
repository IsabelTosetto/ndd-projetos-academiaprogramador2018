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

            CheckAvailableRoom(scheduling);

            return _repository.Save(scheduling);
        }

        public Scheduling Update(Scheduling scheduling)
        {
            if (scheduling.Id < 1)
                throw new IdentifierUndefinedException();

            scheduling.Validate();

            CheckAvailableRoom(scheduling);

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

        public void CheckAvailableRoom(Scheduling scheduling)
        {
            IEnumerable<Scheduling> schedulings = _repository.GetAll();

            foreach (Scheduling s in schedulings)
            {
                if (s.Room == scheduling.Room)
                {
                    if (s.StartTime.Day == scheduling.StartTime.Day)
                    {
                        if (s.StartTime.Hour == scheduling.StartTime.Hour
                            || (scheduling.StartTime.Hour > s.StartTime.Hour && scheduling.StartTime.Hour < s.EndTime.Hour)
                            || (scheduling.EndTime.Hour > s.StartTime.Hour && scheduling.EndTime.Hour < s.EndTime.Hour)
                            || (scheduling.StartTime.Hour < s.StartTime.Hour && scheduling.EndTime.Hour > s.EndTime.Hour)
                            || (scheduling.StartTime.Hour < s.StartTime.Hour && scheduling.EndTime.Hour > s.StartTime.Hour))
                        {
                            throw new SchedulingUnavailableRoomException();
                        }
                    }
                }
            }
        }
    }
}
