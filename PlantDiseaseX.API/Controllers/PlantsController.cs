using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlantDiseaseX.API.Dtos;
using PlantDiseaseX.API.Errors;
using PlantDiseaseX.API.Helpers;
using PlantDiseaseX.Core.Entities;
using PlantDiseaseX.Core.Repositories;
using PlantDiseaseX.Core.Specifications;


namespace PlantDiseaseX.API.Controllers
{
    
    public class PlantsController : APIBaseController
    {
        private readonly IGenericRepository<Plant> _plantRepo;
        private readonly IGenericRepository<Season> _seasonsRepo;
        private readonly IGenericRepository<Plantcategory> _categoriesRepo;
        private readonly IMapper _mapper;

        public PlantsController(
            IGenericRepository<Plant> plantRepo,
            IGenericRepository<Plantcategory> categoriesRepo,
            IGenericRepository<Season> seasonsRepo,
            IMapper mapper)
        {
            _plantRepo = plantRepo;
            _categoriesRepo = categoriesRepo;
            _seasonsRepo = seasonsRepo;
            _mapper = mapper;
        }

        //[CachedAttribute(600)]
        [HttpGet]
        public async Task<ActionResult<Pagination<PlantToReturnDto>>> GetPlants([FromQuery]PlantSpecParams specParams)
        {
            var spec = new PlantWithCategoryAndSeasonSpecification(specParams);

            var plants = await _plantRepo.GetAllWithSpecAsync(spec);


            var data = _mapper.Map<IReadOnlyList<Plant>, IReadOnlyList<PlantToReturnDto>>(plants);

            var countSpec = new PlantWithFilterationForCountSpecification(specParams);

            var count = await _plantRepo.GetCountWithSpecAsync(countSpec);

            return Ok(new Pagination<PlantToReturnDto>(specParams.PageIndex , specParams.PageSize, count, data));
        }


        [ProducesResponseType(typeof(PlantToReturnDto) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<PlantToReturnDto>> GetPlant(int id)
        {
            var spec = new PlantWithCategoryAndSeasonSpecification(id);

            var plant = await _plantRepo.GetByIdWithSpecAsync(spec);

            if (plant is null) return NotFound(new ApiResponse(404));


            return Ok(_mapper.Map<Plant, PlantToReturnDto>(plant));
        }


        [HttpGet("categories")]    // Get : api/plants/categories
        public async Task<ActionResult<IReadOnlyList<Plantcategory>>> GetCategories()
        {
            var categories = await _categoriesRepo.GetAllAsync();
            return Ok(categories);
        }



        [HttpGet("seasons")]  // Get  : api/plants/seasons
        public async Task<ActionResult<IReadOnlyList<Season>>> GetSeasons()
        {
            var seasons = await _seasonsRepo.GetAllAsync();

            return Ok(seasons);
        }

    }
}
