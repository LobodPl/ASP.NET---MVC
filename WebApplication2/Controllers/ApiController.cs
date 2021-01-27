using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestApiController : ControllerBase
    {
        private EFProductRepository EFPR;

        public RestApiController(AppDbContext ctx)
        {
            this.EFPR = new EFProductRepository(ctx);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Product> Create(Product product)
        {
            EFPR.SaveItem(product);
            return CreatedAtAction(nameof(getProductByName), new { id = product.ProductId }, product);
        }

        [HttpGet]
        public ActionResult<List<Product>> getProducts()
        {
            List<Product> products = EFPR.Products.AsEnumerable<Product>().ToList();
            if (products == null)
            {
                return NotFound();
            }

            return products;
        }

        [HttpGet("{id}")]
        public ActionResult<Product> getProductByName(int id)
        {
            Product product = EFPR.Products.First(x => x.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Product> editProcuct(int id, Product product)
        {
            if (id != product.ProductId) return BadRequest();
            EFPR.SaveItem(product);

            return AcceptedAtAction(nameof(getProductByName), new { id = product.ProductId }, product);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Product> deleteProcuctById(int productId)
        {
            if (EFPR.Products.First(x => x.ProductId == productId) == null) return NotFound();
            EFPR.DeleteItem(productId);

            return NoContent();
        }
    }
}