﻿using DataBaseLayer.models;
using DTO;
using DTO.Constant;
using DTO.Entities.Product;
using DTO.Entities.RelatedWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Photos.Helper;
using ServiceLayer.Products.Helper;
using ServiceLayer.RelatedWorks.Helper;
using ServiceLayer.Shared;

namespace Core.Controllers
{
    [Route("AdminECommerce/RelatedWork")]
    [ApiController]
   [Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.Admin)]
    public class AdminRelatedWorkController : ControllerBase
    {
       private readonly IRelatedWorkSerivce _related;
        private readonly IFileImageUploading _Upload;
        private readonly IProductService _product;
        public AdminRelatedWorkController(IRelatedWorkSerivce related, IFileImageUploading upload, IProductService product)
        {
            _related = related;
            _Upload = upload;
            _product = product;
        }

        [HttpGet("GetAll")]
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
        public async Task<IActionResult> insert([FromForm] AddRelatedWorkDTO Work)
        {
            try
            {
                AddProductDTO product = await _product.get(Work.ProductId);
                if (product == null)
                {
                    return BadRequest("Product :" + Errors.NotFound);
                }
                string photo;
                if (_Upload.UploadPhoto(Work, out photo))
                {
                    Work.SetPhotoUrl(photo);
                }
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
        public async Task<IActionResult> update(int WorkID, [FromForm] AddRelatedWorkDTO relatedWorkDTO)
        {
            try
            {
                if (WorkID != relatedWorkDTO.ID)
                    return BadRequest("الارقام التعريفيه غير متساويه");
                AddProductDTO product = await _product.get(relatedWorkDTO.ProductId);
                if (product == null)
                {
                    return BadRequest("Product :" + Errors.NotFound);
                }
                string photo;
                if (_Upload.UploadPhoto(relatedWorkDTO, out photo))
                {
                    relatedWorkDTO.SetPhotoUrl(photo);
                }
                var IsUpdated = await _related.Update(WorkID, relatedWorkDTO);
                return IsUpdated == true ? Ok() : NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
