using DataBaseLayer.models;
using DTO.Entities.Message;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Message.Helper;

namespace Core.Controllers.Message
{
    [Route("ECommerce/[controller]")]
    [ApiController]
    //[Authorize]
    public class MessagesController : ControllerBase
    {
        private readonly IMessage _message;

        public MessagesController(IMessage message)
        {
            _message = message;
        }
        [HttpGet("GetAll/")]
        public async Task<IActionResult> Get(int index  = 0, int size = 20)
        {
            return Ok( await _message.GetMessages(index, size));
        }
        [HttpGet("Get/")]
       
        public async Task<IActionResult> Get(Guid id)
        {
            MessageDTO message = await _message.Get(id);
            return  message == null ?  NotFound(): Ok(message);
        }
        [HttpPost("Add/")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(MessageDTO message)
        {
            try
            {
               await _message.Insert(message);

                return Ok();
            }catch (Exception ex)
            {
                return BadRequest(message);
            }
        }
    }
}
