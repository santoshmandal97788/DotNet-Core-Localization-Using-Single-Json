using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace LocalizationUsingSingleJsonFile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalizationController : BaseController
    {
        public LocalizationController(IStringLocalizer localizer) : base(localizer)
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            var message = _localizer["nkcpykswzb"].Value;
            return Ok(message);
        }
    }
}
