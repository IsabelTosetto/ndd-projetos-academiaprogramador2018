using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonaLaura.Domain.Features.Sales;

namespace DonaLaura.Application.Features.Sales
{
    public class SaleService : ISaleService
    {
        ISaleRepository _repositorio;

        public SaleService(ISaleRepository repository)
        {
            _repositorio = repository;
        }

        public Sale Add(Sale sale)
        {
            throw new NotImplementedException();
        }

        public void Delete(Sale sale)
        {
            throw new NotImplementedException();
        }

        public Sale Get(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Sale> GetAll()
        {
            throw new NotImplementedException();
        }

        public Sale Update(Sale sale)
        {
            throw new NotImplementedException();
        }
    }
}
