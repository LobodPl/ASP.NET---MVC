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

        //POST api/RestApi
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Product> Create(Product product)
        {
            EFPR.SaveItem(product);
            return CreatedAtAction(nameof(getProductById), new { id = product.ProductId }, product);
        }

        //GET api/RestApi
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

        //GET api/RestApi/{id}
        [HttpGet("{id}")]
        public ActionResult<Product> getProductById(int id)
        {
            Product product = EFPR.Products.First(x => x.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        //PUT api/RestApi/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Product> editProcuct(int id, Product product)
        {
            if (id != product.ProductId) return BadRequest();
            EFPR.SaveItem(product);

            return AcceptedAtAction(nameof(getProductById), new { id = product.ProductId }, product);
        }


        //DELETE api/RestApi/{id}
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