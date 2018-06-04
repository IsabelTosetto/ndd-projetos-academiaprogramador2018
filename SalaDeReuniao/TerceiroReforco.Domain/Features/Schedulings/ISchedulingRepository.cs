using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerceiroReforco.Domain.Features.Schedulings
{
    public interface ISchedulingRepository
    {
        Scheduling Save(Scheduling scheduling);
        Scheduling Update(Scheduling scheduling);
        Scheduling Get(long id);
        IEnumerable<Scheduling> GetAll();
        bool Delete(Scheduling scheduling);
        bool CheckAvailableRoom(Scheduling scheduling);
    }
}
