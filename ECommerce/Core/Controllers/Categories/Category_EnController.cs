using DTO.Entities.Category;
using DTO.Enums;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Categories.Helper;
using ServiceLayer.Photos.Helper;
using ServiceLayer.Shared;

namespace Core.Controllers.Categories
{
    [ApiController]
    [Route("en/ECommerce/Category")]
    public class Category_EnController : ControllerBase
    {


        private readonly ICategoryService _category;
        private readonly IFileImageUploading _Upload;

        public Category_EnController(ICategoryService category, IFileImageUploading upload)
        {
            _category = category;
            _Upload = upload;
        }

        [HttpGet("GetAll/")]
        public async Task<IActionResult> Get(int index = 0, int size = 20)
        {
            return Ok(await _category.getAll(LanguageCode.en, index, size));
        }
        [HttpGet("Get/")]
        public async Task<IActionResult> Get(int Id)
        {
            var Category = await _category.get(LanguageCode.en, Id);

            return Category != null ? Ok(Category) : NotFound(Errors.NotFound);
        }
       
    }
}