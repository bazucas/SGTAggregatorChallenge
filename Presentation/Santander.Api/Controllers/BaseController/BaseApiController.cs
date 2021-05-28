using Microsoft.AspNetCore.Mvc;

namespace Santander.Api.Controllers.BaseController
{
        [Route("api/[controller]")]
        [ApiController]
        public abstract class BaseApiController : ControllerBase
        {

        }
}