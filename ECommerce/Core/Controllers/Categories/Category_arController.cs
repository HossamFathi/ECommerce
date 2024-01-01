using DTO.Entities.Category;
using DTO.Enums;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Categories.Helper;
using ServiceLayer.Photos.Helper;
using ServiceLayer.Shared;

namespace Core.Controllers.Categories
{
    [ApiController]
    [Route("ar/ECommerce/Category")]
    public class Category_arController : ControllerBase
    {


        private readonly ICategoryService _category;
        private readonly IFileImageUploading _Upload;

        public Category_arController(ICategoryService category, IFileImageUploading upload)
        {
            _category = category;
            _Upload = upload;
        }

       
        [HttpGet("GetAll/")]
        public async Task<IActionResult> GetArabic(int index = 0, int size = 20)
        {
            return Ok(await _category.getAll(LanguageCode.ar, index, size));
        }
        [HttpGet("Get/")]
        public async Task<IActionResult> GetArabic(int Id)
        {
            var Category = await _category.get(LanguageCode.ar, Id);

            return Category != null ? Ok(Category) : NotFound(Errors.NotFound);
        }
       
    }
}