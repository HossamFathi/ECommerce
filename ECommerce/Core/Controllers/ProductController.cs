using DTO;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Categories.Helper;
using ServiceLayer.Products.Helper;
using ServiceLayer.Shared;

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

        [HttpGet("GetAll/")]
        public async Task<IActionResult> Get(int index =0, int size=20)
        {
            return Ok(await _products.getAll(index, size));
        }
        [HttpGet("Get/")]
        public async Task<IActionResult> Get(int ProductID)
        {
            var Product = await _products.get(ProductID);
            return Product != null ?  Ok(Product) : NotFound(Errors.NotFound);
        }
        [HttpGet("getproduct/{CategoryID}")]
        public async Task<IActionResult> GetAllForCategory(int CategoryID)
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

            return await _products.Delete(productID) == true ? Ok() : NotFound(Errors.NotFound);

        }
    }
}

