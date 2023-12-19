using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Photos.Helper;
using ServiceLayer.RelatedWorks.Helper;


namespace Core.Controllers
{
    [Route("ECommerce/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IPhotoService _Photos;
        public PhotosController(IPhotoService Photos)
        {
            _Photos = Photos;
        }
        
       
        [HttpPost("add")]
        public async Task<IActionResult> insert([FromForm]PhotoDTO photo)
        {
            try
            {
                await _Photos.Insert(photo);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
      
        [HttpDelete("delete/{photoID}")]
        public async Task<IActionResult> delete(int photoID)
        {
            var photo = await _Photos.Delete(photoID);
            return photo == true ? Ok(photo) : NotFound();
        }

    }
}
