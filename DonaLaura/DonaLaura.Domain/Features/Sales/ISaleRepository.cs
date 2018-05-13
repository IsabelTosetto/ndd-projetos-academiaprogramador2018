using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Domain.Features.Sales
{
    public interface ISaleRepository
    {
        Sale Save(Sale sale);
        Sale Update(Sale sale);
        Sale Get(long id);
        IEnumerable<Sale> GetAll();
        void Delete(Sale sale);
    }
}
