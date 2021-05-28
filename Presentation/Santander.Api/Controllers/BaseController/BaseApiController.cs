using Microsoft.AspNetCore.Mvc;

namespace Santander.Api.Controllers.BaseController
{
    public class BaseApiController
    {
        [Route("api/[controller]")]
        [ApiController]
        public abstract class BaseApiController : ControllerBase
        {

        }
    }
}