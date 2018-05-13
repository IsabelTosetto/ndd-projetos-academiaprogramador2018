using DonaLaura.Domain.Features.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Application.Features.Sales
{
    public interface ISaleService
    {
        Sale Add(Sale sale);
        Sale Update(Sale sale);
        Sale Get(long id);
        IEnumerable<Sale> GetAll();
        void Delete(Sale sale);
    }
}
