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
        private readonly IFileImageUploading _Upload;
        public PhotosController(IPhotoService Photos, IFileImageUploading upload)
        {
            _Photos = Photos;
            _Upload = upload;
        }


        [HttpPost("add")]
        public async Task<IActionResult> insert([FromForm]PhotoDTO photo)
        {
            try
            {
                string path = "";
                if (_Upload.UploadPhoto(photo, out path))
                {
                   photo.path = path;
                }
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
