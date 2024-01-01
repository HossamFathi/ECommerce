using DTO;
using DTO.Entities.Product;
using DTO.Enums;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Categories.Helper;
using ServiceLayer.Products.Helper;
using ServiceLayer.Shared;

namespace Core.Controllers
{
    [ApiController]
    [Route("en/ECommerce/product")]
    public class Product_enController : ControllerBase
    {
        private readonly IProductService _products;

        public Product_enController(IProductService products)
        {
            _products = products;
        }

        
        [HttpGet("GetAll/")]
        public async Task<IActionResult> GetEng(int index = 0, int size = 20)
        {
            return Ok(await _products.getAll(LanguageCode.en,index, size));
        }
        [HttpGet("Get/")]
        public async Task<IActionResult> Get(int ProductID)
        {
            var Product = await _products.get(LanguageCode.en, ProductID);
            return Product != null ?  Ok(Product) : NotFound(Errors.NotFound);
        }
       
        [HttpGet("{CategoryID}")]
        public async Task<IActionResult> GetAllForCategory(int CategoryID)
        {
            return Ok(await _products.getAll(LanguageCode.en,CategoryID));
        }
        
    }
}

