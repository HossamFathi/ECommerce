using DTO;
using DTO.Entities.Setting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.RelatedWorks.Helper;
using ServiceLayer.Settings.Helper;

namespace Core.Controllers.Setting
{
    [Route("ECommerce/[controller]")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly ISettingService _setting;
        public SettingController(ISettingService setting)
        {
            _setting = setting;
        }

        [HttpGet("get")]
        public async Task<IActionResult> get()
        {
            var Setting = await _setting.get();
            return Setting == null ? NotFound() : Ok(Setting);
        }
        [HttpPost("add")]
        public async Task<IActionResult> insert(SettingDTO setting)
        {
            try
            {
                await _setting.Insert(setting);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        [HttpPut("update/{settingID}")]
        public async Task<IActionResult> update(int settingID, SettingDTO setting)
        {
            try
            {
                if (settingID != setting.ID)
                    return BadRequest("الارقام التعريفيه غير متساويه");
                var IsUpdated = await _setting.Update(settingID, setting);
                return IsUpdated == true ? Ok("تم التعديل بنجاح") : NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        [HttpDelete("delete/{SettingID}")]
        public async Task<IActionResult> delete(int SettingID)
        {
            var Setting = await _setting.Delete(SettingID);
            return Setting == true ? Ok(Setting) : NotFound();
        }

    }
}
