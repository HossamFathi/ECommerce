using DTO;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Categories.Helper;

namespace Core.Controllers
{
    [ApiController]
    [Route("ECommerce/[controller]")]
    public class CategoryController : ControllerBase
    {
        

        private readonly ICategoryService _category;

        public CategoryController(ICategoryService category)
        {
            _category = category;
        }

        [HttpGet("GetAll/")]
        public async Task<IActionResult> Get()
        {
           return Ok(await _category.getAll(0, 20));
        }
        [HttpPost("add/")]
        public async Task<IActionResult> add(CategoryDTO category)
        {
            if (ModelState.IsValid) 
            {
                await _category.Insert(category);
                return Ok();
            }
            return BadRequest(category);
        }
        [HttpDelete("/Delete/{categoryID}")]
        public async Task<IActionResult> Delete(int categoryID)
        {
           
                return await _category.Delete(categoryID) == true ?  Ok() : NotFound();
            
        }
    }
}