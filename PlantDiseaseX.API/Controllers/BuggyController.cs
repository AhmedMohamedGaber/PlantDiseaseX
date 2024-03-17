using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlantDiseaseX.API.Errors;
using PlantDiseaseX.Repository.Data;

namespace PlantDiseaseX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : APIBaseController
    {
        private readonly PlantContext _dbContext;
        public BuggyController(PlantContext dbContext) 
        {
            _dbContext = dbContext;
        }


        [HttpGet("notfound")]  // Get : api/buggy/notfound

        public ActionResult GetNotFoundRequest()
        {
            var plant = _dbContext.Plants.Find(100);
            if(plant is null) return NotFound(new ApiResponse(404));

            return Ok(plant);
        }

        [HttpGet("servererror")]  // Get : api/buggy/servererror

        public ActionResult GetServerError()
        {
            var plant = _dbContext.Plants.Find(100);
            var plantToReturn = plant.ToString();


            return Ok(plantToReturn);
        }


        [HttpGet("badrequest")]  // Get :  api/buggy/badrequest

        public ActionResult GetBadRequest() 
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]  // Get : api/buggy/badrequest/5

        public ActionResult GetBadRequest(int id) // validation error
        {
            return Ok();
        }

    }
}
