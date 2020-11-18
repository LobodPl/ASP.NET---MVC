using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Components;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ProductController : Controller
    {
        private EFProductRepository EFPR;

        public ProductController(AppDbContext ctx)
        {
            this.EFPR = new EFProductRepository(ctx);
        }

        public IActionResult Index()
        {
            return View(this.EFPR.Products);
        }

        public IActionResult List(string category)
        {
            return View(this.EFPR.Products.Where(x => x.Category.Name == category));
        }
    }
}