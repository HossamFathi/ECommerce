using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.RelatedWorks.Helper;

namespace Core.Controllers
{
    [Route("ECommerce/[controller]")]
    [ApiController]
    public class RelatedWorkController : ControllerBase
    {
       private readonly IRelatedWorkSerivce _related;
        public RelatedWorkController(IRelatedWorkSerivce related)
        {
            _related = related;
        }
        [HttpGet("GetAllForProduct/{ProductID}")]
        public async Task<IActionResult> GetAllForProduct(int ProductID) {
            return Ok(await _related.getAll(ProductID));
        }
        [HttpGet("GetAllForProduct")]
        public async Task<IActionResult> GetAll(int index =0, int size= 10)
        {
            return Ok(await _related.getAll(index , size));
        }
        [HttpGet("get/{WorkID}")]
        public async Task<IActionResult> get(int WorkID)
        {
             var Work = await _related.get(WorkID);
            return Work == null ? NotFound() : Ok(Work);
        }
        [HttpPost("add")]
        public async Task<IActionResult> insert(RelatedWorkDTO Work)
        {
            try
            {
                await _related.Insert(Work);
                return  Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        
        }
        [HttpDelete("delete/{WorkID}")]
        public async Task<IActionResult> delete(int WorkID)
        {
            var Work = await _related.Delete(WorkID);
            return Work == true ? NotFound() : Ok(Work);
        }
        [HttpPut("update/{WorkID}")]
        public async Task<IActionResult> update(int WorkID, RelatedWorkDTO relatedWorkDTO)
        {
            try
            {
                if (WorkID != relatedWorkDTO.ID)
                    return BadRequest("الارقام التعريفيه غير متساويه");
                var IsUpdated = await _related.Update(WorkID, relatedWorkDTO);
                return IsUpdated == true ? Ok("تم التعديل بنجاح") : NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
