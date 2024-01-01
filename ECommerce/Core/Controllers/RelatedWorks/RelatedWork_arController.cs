using DTO;
using DTO.Entities.RelatedWork;
using DTO.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.RelatedWorks.Helper;

namespace Core.Controllers.RelatedWorks
{
    [Route("ar/ECommerce/RelatedWork")]
    [ApiController]
    public class RelatedWork_arController : ControllerBase
    {
        private readonly IRelatedWorkSerivce _related;
        public RelatedWork_arController(IRelatedWorkSerivce related)
        {
            _related = related;
        }
        [HttpGet("GetAllForProduct/{ProductID}")]
        public async Task<IActionResult> GetAllForProduct(int ProductID)
        {
            return Ok(await _related.getAll(ProductID , LanguageCode.ar));
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(int index = 0, int size = 10)
        {
            return Ok(await _related.getAll(index, size, LanguageCode.ar));
        }
       
    }
}
