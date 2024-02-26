using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlantDiseaseX.Core.Entities;
using PlantDiseaseX.Core.Repositories;

namespace PlantDiseaseX.API.Controllers
{
    
    public class PlantsController : APIBaseController
    {
        private readonly IGenericRepository<Plant> _plantRepo;

        public PlantsController(IGenericRepository<Plant> plantRepo)
        {
          _plantRepo = plantRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plant>>> GetPlants()
        {
            var plants = await _plantRepo.GetAllAsync();

            return Ok(plants);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Plant>> GetPlant(int id)
        {
            var plant = await _plantRepo.GetByIdAsync(id);
            return Ok(plant);
        }

    }
}
