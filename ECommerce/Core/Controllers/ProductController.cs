using DTO;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Categories.Helper;
using ServiceLayer.Products.Helper;

namespace Core.Controllers
{
    [ApiController]
    [Route("ECommerce/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _products;

        public ProductController(IProductService products)
        {
            _products = products;
        }

        [HttpGet("getall/")]
        public async Task<IActionResult> GetProd()
        {
            return Ok(await _products.getAll(0, 20));
        }
        [HttpGet("getproduct/{CategoryID}")]
        public async Task<IActionResult> Get(int CategoryID)
        {
            return Ok(await _products.getAll(CategoryID));
        }
        [HttpPost("Add/")]
        public async Task<IActionResult> add(ProductDTO product)
        {
            if (ModelState.IsValid)
            {
                await _products.Insert(product);
                return Ok();
            }
            return BadRequest(product);
        }
        [HttpDelete("delete/")]
        public async Task<IActionResult> Delete(int productID)
        {

            return await _products.Delete(productID) == true ? Ok() : NotFound();

        }
    }
}

