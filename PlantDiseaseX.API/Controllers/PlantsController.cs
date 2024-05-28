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
using PlantDiseaseX.Core;
using PlantDiseaseX.API.Dtos;
using PlantDiseaseX.Core.Repositories;
using System.Numerics;
using PlantDiseaseX.API.Dtos;



namespace PlantDiseaseX.API.Controllers
{
    
    public class PlantsController : APIBaseController
    {
       


        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PlantsController(
           
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
           
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<PlantToReturnDto>> CreatePlant([FromBody] PlantCreateDto plantCreateDto)
        {
            // Check if the incoming DTO is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map the incoming DTO to the Plant entity
            var plantToCreate = _mapper.Map<PlantCreateDto, Plant>(plantCreateDto);

            // Add the new plant entity to the database
            _unitOfWork.Repository<Plant>().AddAsync(plantToCreate);
            await _unitOfWork.Complete();

            // Map the created plant entity back to DTO for response
            var plantToReturnDto = _mapper.Map<Plant, PlantToReturnDto>(plantToCreate);

            // Return the newly created plant DTO
            return CreatedAtAction(nameof(GetPlant), new { id = plantToReturnDto.Id }, plantToReturnDto);
        }

        //[CachedAttribute(600)]
        [HttpGet]
        public async Task<ActionResult<Pagination<PlantToReturnDto>>> GetPlants([FromQuery]PlantSpecParams specParams)
        {
            var spec = new PlantWithCategoryAndSeasonSpecification(specParams);

            var plants = await _unitOfWork.Repository<Plant>().GetAllWithSpecAsync(spec);


            var data = _mapper.Map<IReadOnlyList<Plant>, IReadOnlyList<PlantToReturnDto>>(plants);

            var countSpec = new PlantWithFilterationForCountSpecification(specParams);

            var count = await _unitOfWork.Repository<Plant>().GetCountAsync(countSpec);

            return Ok(new Pagination<PlantToReturnDto>(specParams.PageIndex , specParams.PageSize, count, data));
        }


        [ProducesResponseType(typeof(PlantToReturnDto) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<PlantToReturnDto>> GetPlant(int id)
        {
            var spec = new PlantWithCategoryAndSeasonSpecification(id);

            var plant = await _unitOfWork.Repository<Plant>().GetByIdWithSpecAsync(spec);

            if (plant is null) return NotFound(new ApiResponse(404));

            var plantToReturnDto = _mapper.Map<Plant, PlantToReturnDto>(plant);

            var responseDto = new PlantResponseDto
            {
                Data = plantToReturnDto
            };

            return Ok(responseDto);
          //  return Ok(_mapper.Map<Plant, PlantToReturnDto>(plant,responseDto));
        }




        [HttpPut("{id}")]
        public async Task<ActionResult<PlantToReturnDto>> UpdatePlant(int id, [FromBody] PlantUpdateDto plantUpdateDto)
        {
            // Check if the incoming DTO is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Get the existing plant entity from the database
            var existingPlant = await _unitOfWork.Repository<Plant>().GetByIdAsync(id);

            // If the plant with the provided id doesn't exist, return a Not Found response
            if (existingPlant == null)
            {
                return NotFound();
            }

            // Map the properties from the update DTO to the existing plant entity
            _mapper.Map(plantUpdateDto, existingPlant);

            // Update the plant entity in the database
            _unitOfWork.Repository<Plant>().Update(existingPlant);
            await _unitOfWork.Complete();

            // Map the updated plant entity back to DTO for response
            var updatedPlantDto = _mapper.Map<PlantToReturnDto>(existingPlant);

            // Return the updated plant DTO
            return Ok(updatedPlantDto);
        }

        // New DELETE endpoint
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePlant(int id)
        {
            // Get the plant entity from the database
            var plantToDelete = await _unitOfWork.Repository<Plant>().GetByIdAsync(id);

            // If the plant with the provided id doesn't exist, return a Not Found response
            if (plantToDelete == null)
            {
                return NotFound();
            }

            // Delete the plant entity from the database
            _unitOfWork.Repository<Plant>().Delete(plantToDelete);
            await _unitOfWork.Complete();

            // Return a No Content response indicating successful deletion
            return NoContent();
        }








        [HttpGet("categories")]    // Get : api/plants/categories
        public async Task<ActionResult<IReadOnlyList<Plantcategory>>> GetCategories()
        {
            var categories = await _unitOfWork.Repository<Plantcategory>().GetAllAsync();

            var responseDto = new PlantCategoryResponseDto
            {
                Data = categories
            };

            return Ok(responseDto);
            //   return Ok(categories);
        }



        [HttpGet("seasons")]  // Get  : api/plants/seasons
        public async Task<ActionResult<IReadOnlyList<Season>>> GetSeasons()
        {
            var seasons = await _unitOfWork.Repository<Season>().GetAllAsync();
            var responseDto = new SeasonResponseDto
            {
                Data = seasons
            };

            return Ok(responseDto);
            // return Ok(seasons);
        }

    }
}
