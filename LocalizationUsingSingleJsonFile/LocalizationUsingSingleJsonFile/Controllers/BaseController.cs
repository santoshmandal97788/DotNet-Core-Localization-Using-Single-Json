using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace LocalizationUsingSingleJsonFile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IStringLocalizer _localizer;
        public BaseController(IStringLocalizer localizer)
        {
            _localizer = localizer;
        }
    }
}
