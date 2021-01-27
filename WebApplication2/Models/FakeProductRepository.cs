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

        public IQueryable<Category> Categories => throw new NotImplementedException();

        IQueryable<Product> IProductRepository.Products => this.Products;

        public void DeleteItem(int itemId)
        {
            throw new NotImplementedException();
        }

        public void SaveItem(Product product)
        {
            throw new NotImplementedException();
        }
    }
}