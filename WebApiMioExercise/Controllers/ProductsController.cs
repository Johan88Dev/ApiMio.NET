using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiMioExercise.Models;

namespace WebApiMioExercise.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {

        private readonly ProductsDb _dbContext;

        public ProductsController(ProductsDb dbContext)
        {
            _dbContext = dbContext;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> Get()
        {
            await Task.Delay(200);

            if (_dbContext.Products == null)
            {
                return NotFound();
            }

            return await _dbContext.Products.ToListAsync();
        }

        [HttpGet("/products/{id}")]
        public async Task<ActionResult<Products>> GetProduct(string id)
        {
            if (_dbContext.Products == null)
            {
                return NotFound();
            }

            var product = await _dbContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Products>> PostProduct(Products products)
        {
            _dbContext.Products.Add(products);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = products.Id }, products);
        }

        [HttpPut("/products/{id}")]
        public async Task<IActionResult> PutProduct(string id, Products products)
        {
            if (id != products.Id)
            {
                return BadRequest();
            }
            _dbContext.Entry(products).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        private bool ProductExists(string id)
        {
            return (_dbContext.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        [HttpDelete("/products/{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            if (_dbContext.Products == null)
            {
                return NotFound();
            }

            var product = await _dbContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}