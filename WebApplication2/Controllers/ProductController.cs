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
        private IProductRepository EFPR;

        public ProductController(IProductRepository repo)
        {
            EFPR = repo;
        }

        public IActionResult Index()
        {
            return View(this.EFPR.Products);
        }

        public IActionResult List(string category)
        {
            return View(this.EFPR.Products.Where(x => x.Category.Name == category));
        }

        public ViewResult GetItemById(int id) => View(EFPR.Products.Single(x => x.ProductId == id));
    }
}