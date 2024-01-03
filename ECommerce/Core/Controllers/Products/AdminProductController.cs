using DTO;
using DTO.Constant;
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
    [Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.Admin)]
    public class AdminProductController : ControllerBase
    {
        private readonly IProductService _products;

        public AdminProductController(IProductService products)
        {
            _products = products;
        }

        [HttpGet("GetAll/")]
        public async Task<IActionResult> Get(int index = 0, int size = 20)
        {
            return Ok(await _products.getAll(index, size));
        }
       
        [HttpPost("Add/")]
        public async Task<IActionResult> add(AddProductDTO product)
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
        [HttpPut("Update/")]
        public async Task<IActionResult> Update(int productID , AddProductDTO productDTO)
        {
            if (!ModelState.IsValid || productDTO.Id != productID) { return BadRequest(productDTO); }
           
            return await _products.Update(productID , productDTO) == true ? Ok() : NotFound(Errors.NotFound);

        }
    }
}

