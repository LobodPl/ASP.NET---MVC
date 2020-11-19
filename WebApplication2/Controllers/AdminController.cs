using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class AdminController : Controller
    {
        private EFProductRepository EFPR;

        public AdminController(AppDbContext ctx)
        {
            this.EFPR = new EFProductRepository(ctx);
        }

        public IActionResult Index()
        {
            return View(EFPR.Products);
        }

        public IActionResult Edit(int productId)
        {
            EditProductModel model = new EditProductModel();
            model.product = EFPR.Products.FirstOrDefault(x => x.ProductId == productId);
            model.Categories = EFPR.Categories;
            return View(model);
        }

        [HttpPost]
        public IActionResult Save(EditProductModel productmodel)
        {
            Product product = productmodel.product;
            if (ModelState.IsValid)
            {
                EFPR.SaveItem(product);
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = "Podane parametry są nie poprawne";
                return View("Edit", productmodel);
            }
        }

        public IActionResult Create()
        {
            EditProductModel model = new EditProductModel();
            model.product = new Product();
            model.Categories = EFPR.Categories;
            return View("Edit", model);
        }

        [HttpPost]
        public IActionResult Delete(int ProductId)
        {
            EFPR.DeleteItem(ProductId);
            TempData["message"] = "Product został usunięty.";
            return View("Index", EFPR.Products);
        }
    }
}