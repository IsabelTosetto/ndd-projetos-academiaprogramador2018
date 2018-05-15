using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonaLaura.Domain.Exceptions;
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
            sale.Validate();

            return _repositorio.Save(sale);
        }

        public Sale Update(Sale sale)
        {
            if (sale.Id < 1)
                throw new IdentifierUndefinedException();

            sale.Validate();

            return _repositorio.Update(sale);
        }

        public Sale Get(long id)
        {
            if (id < 1)
                throw new IdentifierUndefinedException();

            return _repositorio.Get(id);
        }

        public IEnumerable<Sale> GetAll()
        {
            return _repositorio.GetAll();
        }
        
        public void Delete(Sale sale)
        {
            if (sale.Id < 1)
                throw new IdentifierUndefinedException();

            _repositorio.Delete(sale);
        }
    }
}
