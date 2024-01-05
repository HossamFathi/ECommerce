using DTO.Constant;
using DTO.Entities.Category;
using DTO.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Categories.Helper;
using ServiceLayer.Photos.Helper;
using ServiceLayer.Shared;

namespace Core.Controllers.Categories
{
    [ApiController]
    [Route("Admin/ECommerce/Category")]
    //[Authorize(AuthenticationSchemes = "Bearer" , Roles = Roles.Admin)]
    public class AdminCategoryController : ControllerBase
    {


        private readonly ICategoryService _category;
        private readonly IFileImageUploading _Upload;

        public AdminCategoryController(ICategoryService category, IFileImageUploading upload)
        {
            _category = category;
            _Upload = upload;
        }

        [HttpGet("GetAll/")]
        public async Task<IActionResult> Get(int index = 0, int size = 20)
        {
            return Ok(await _category.getAll(index, size));
        }
        [HttpGet("Get/")]
        public async Task<IActionResult> Get(int ID)
        {
            AddCategoryDTO category = await _category.get(ID); 
            
            return category == null ? NotFound(Errors.NotFound) :  Ok(category);
        }
        [HttpPost("add/")]
        public async Task<IActionResult> add([FromForm] AddCategoryDTO category)
        {
            if (ModelState.IsValid)
            {
                string photo;
                if (_Upload.UploadPhoto(category, out photo))
                {
                    category.SetPhotoPath(photo);
                }
                await _category.Insert(category);
                return Ok();
            }
            return BadRequest(category);
        }
        [HttpPut("Update/{CategoryID}")]
        public async Task<IActionResult> Update(int CategoryID, [FromForm] AddCategoryDTO category)
        {
            if (ModelState.IsValid)
            {
                if (CategoryID != category.ID)
                    return BadRequest(Errors.NotTheSameID);
                string photo;
                if (_Upload.UploadPhoto(category, out photo))
                {
                    category.SetPhotoPath(photo);
                }
                return await _category.Update(CategoryID, category) == false ? NotFound(Errors.NotFound) : Ok();

            }
            return BadRequest(category);
        }
        [HttpDelete("Delete/{categoryID}")]
        public async Task<IActionResult> Delete(int categoryID)
        {

            return await _category.Delete(categoryID) == true ? Ok() : NotFound(Errors.NotFound);

        }
    }
}