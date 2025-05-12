using DTO.Constant;
using DTO.Entities.Photo;
using DTO.Entities.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Photos.Helper;
using ServiceLayer.Products.Helper;
using ServiceLayer.RelatedWorks.Helper;
using ServiceLayer.Shared;

namespace Core.Controllers.Photos
{
    [Route("ECommerce/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.Admin)]
    public class PhotosController : ControllerBase
    {
        private readonly IPhotoService _Photos;
        private readonly IFileImageUploading _Upload;
        private readonly IProductService _product;

        public PhotosController(IPhotoService Photos, IFileImageUploading upload, IProductService product)
        {
            _Photos = Photos;
            _Upload = upload;
            _product = product;
        }


        [HttpPost("add")]
        public async Task<IActionResult> insert([FromForm] AddPhotoDTO photo)
        {
            try
            {
                AddProductDTO product =  await _product.get(photo.ProductID);
                if(product == null)
                {
                    return BadRequest("Product :" + Errors.NotFound);
                }

                string path = "";
                if (_Upload.UploadPhoto(photo, out path))
                {
                    photo.SetPath(path);
                }
                await _Photos.Insert(photo);
                return Ok();

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("SetDefault")]

        public async Task<IActionResult> SetDefault(int ID)
        {
            try
            {
                
                await _Photos.SetDefault(ID);
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
