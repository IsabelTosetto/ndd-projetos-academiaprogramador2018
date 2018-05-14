using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonaLaura.Domain.Exceptions;
using DonaLaura.Domain.Features.Products;

namespace DonaLaura.Application.Features.Products
{
    public class ProductService : IProductService
    {
        IProductRepository _repositorio;

        public ProductService(IProductRepository repository)
        {
            _repositorio = repository;
        }

        public Product Add(Product product)
        {
            product.Validate();

            return _repositorio.Save(product);
        }
        public Product Update(Product product)
        {
            if (product.Id < 1)
                throw new IdentifierUndefinedException();

            product.Validate();

            return _repositorio.Update(product);
        }

        public Product Get(long id)
        {
            if (id < 1)
                throw new IdentifierUndefinedException();

            return _repositorio.Get(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _repositorio.GetAll();
        }

        public void Delete(Product product)
        {
            if (product.Id < 1)
                throw new IdentifierUndefinedException();

            _repositorio.Delete(product);
        }
    }
}
