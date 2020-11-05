﻿using Microsoft.EntityFrameworkCore;
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
    }
}