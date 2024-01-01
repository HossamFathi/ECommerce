using DTO;
using DTO.Entities.Product;
using DTO.Enums;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Categories.Helper;
using ServiceLayer.Products.Helper;
using ServiceLayer.Shared;

namespace Core.Controllers.Products
{
    [ApiController]
    [Route("ar/ECommerce/Product")]
    public class Product_arController : ControllerBase
    {
        private readonly IProductService _products;

        public Product_arController(IProductService products)
        {
            _products = products;
        }

        
        [HttpGet("GetAll/")]
        public async Task<IActionResult> GetArabic(int index = 0, int size = 20)
        {
            return Ok(await _products.getAll(LanguageCode.ar, index, size));
        }
       
        [HttpGet("Get/")]
        public async Task<IActionResult> GetArabic(int ProductID)
        {
            var Product = await _products.get(LanguageCode.ar, ProductID);
            return Product != null ? Ok(Product) : NotFound(Errors.NotFound);
        }
       
        [HttpGet("{CategoryID}")]
        public async Task<IActionResult> GetAllForCategoryArabic(int CategoryID)
        {
            return Ok(await _products.getAll(LanguageCode.ar, CategoryID));
        }
       
    }
}

