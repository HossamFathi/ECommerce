using DataBaseLayer.models;
using DTO;
using DTO.Constant;
using DTO.Entities.Category;
using DTO.Entities.Product;
using DTO.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Categories.Helper;
using ServiceLayer.Products.Helper;
using ServiceLayer.Shared;

namespace Core.Controllers
{
    [ApiController]
    [Route("Admin/ECommerce/Product")]
   // [Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.Admin)]
    public class AdminProductController : ControllerBase
    {
        private readonly IProductService _products;
        private readonly ICategoryService _category;

        public AdminProductController(IProductService products, ICategoryService category)
        {
            _products = products;
            _category = category;
        }

        [HttpGet("GetAll/")]
        public async Task<IActionResult> Get(int index = 0, int size = 20)
        {
            return Ok(await _products.getAll(index, size));
        }
        [HttpGet("Get/")]
        public async Task<IActionResult> Get(int ProductID)
        {
            var Product = await _products.get(ProductID);
            return Product != null ? Ok(Product) : NotFound(Errors.NotFound);
        }
        [HttpPost("Add/")]
        public async Task<IActionResult> add(AddProductDTO product)
        {
            if (ModelState.IsValid)
            {
               AddCategoryDTO category = await _category.get(product.CategoryID);
                if(category == null)
                {
                    return BadRequest("Category : "  + Errors.NotFound);
                }

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
        [HttpPut("Update/")]
        public async Task<IActionResult> Update(int productID , AddProductDTO productDTO)
        {
            if (!ModelState.IsValid || productDTO.Id != productID) { return BadRequest(productDTO); }
            AddCategoryDTO category = await _category.get(productDTO.CategoryID);
            if (category == null)
            {
                return BadRequest("Category : " + Errors.NotFound);
            }
            return await _products.Update(productID , productDTO) == true ? Ok() : NotFound(Errors.NotFound);

        }
    }
}

