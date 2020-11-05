using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class FakeProductRepository : IProductRepository
    {
        public IQueryable<Product> Products;

        public FakeProductRepository(IQueryable<Product> List)
        {
            this.Products = List;
        }

        IQueryable<Product> IProductRepository.Products => this.Products;
    }
}