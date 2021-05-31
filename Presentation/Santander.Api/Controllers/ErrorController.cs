using Microsoft.AspNetCore.Mvc;
using Santander.Api.Controllers.BaseController;
using Santander.Api.Errors;

namespace Santander.Api.Controllers
{
    [Route("error/{code:int}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : BaseApiController
    {
        public IActionResult Error(int code)
        {
            // ObjectResult returns IActionResult with content negotiation capability
            return new ObjectResult(new ApiResponse(code));
        }
    }
}
