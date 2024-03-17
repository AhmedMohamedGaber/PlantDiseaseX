using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlantDiseaseX.API.Errors;

namespace PlantDiseaseX.API.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        public ActionResult Error(int code)
        {
            return NotFound(new ApiResponse(code));
        }
    }
}
