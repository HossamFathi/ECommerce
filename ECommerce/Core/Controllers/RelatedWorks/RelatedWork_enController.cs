using DTO;
using DTO.Entities.RelatedWork;
using DTO.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.RelatedWorks.Helper;

namespace Core.Controllers
{
    [Route("en/ECommerce/RelatedWork")]
    [ApiController]
    public class RelatedWork_EnController : ControllerBase
    {
       private readonly IRelatedWorkSerivce _related;
        public RelatedWork_EnController(IRelatedWorkSerivce related)
        {
            _related = related;
        }
        [HttpGet("GetAllForProduct/{ProductID}")]
        public async Task<IActionResult> GetAllForProduct(int ProductID) {
            return Ok(await _related.getAll(ProductID , LanguageCode.en));
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(int index =0, int size= 10)
        {
            return Ok(await _related.getAll(index , size , LanguageCode.en));
        }
        
    }
}
