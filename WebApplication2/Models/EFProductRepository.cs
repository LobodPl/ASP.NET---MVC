using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class EFProductRepository : IProductRepository
    {
        private readonly AppDbContext ctx;

        public EFProductRepository(AppDbContext ctx)
        {
            this.ctx = ctx;
        }

        public IQueryable<Product> Products => ctx.Products.Include(x => x.Category);
        public IQueryable<Category> Categories => ctx.Categories;

        public void SaveItem(Product Product)
        {
            if (Product.ProductId == 0)
            {
                Product.Category = Categories.First(x => x.CategoryId == Product.Category.CategoryId);
                ctx.Products.Add(Product);
            }
            else
            {
                Product temp = ctx.Products.First(x => x.ProductId == Product.ProductId);
                if (temp != null)
                {
                    temp.Name = Product.Name;
                    temp.Desc = Product.Desc;
                    temp.Price = Product.Price;
                    temp.Category = Categories.First(x => x.CategoryId == Product.Category.CategoryId);
                }
            }
            ctx.SaveChanges();
        }

        public void DeleteItem(int itemId)
        {
            Product temp = ctx.Products.First(x => x.ProductId == itemId);
            if (temp != null)
            {
                ctx.Products.Remove(temp);
                ctx.SaveChanges();
            }
        }
    }
}