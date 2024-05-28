using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlantDiseaseX.API.Controllers;
using PlantDiseaseX.Core.Entities;
using PlantDiseaseX.Core;
using plantdiseasex.core.entities;
using PlantDiseaseX.API.Dtos;
using PlantDiseaseX.Core.Specifications;
using PlantDiseaseX.API.Errors;

namespace PlantDiseaseX.API.Controllers
{

    public class CornDiseaseController : APIBaseController
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CornDiseaseController(

          IUnitOfWork unitOfWork,
          IMapper mapper)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        [HttpGet]

        public async Task<ActionResult<IReadOnlyList<CornDiseaseToReturnDto>>> GetNewsArticleS([FromQuery] PlantSpecParams specParams)
        {
            var corndiseases = await _unitOfWork.Repository<corndisease>().GetAllAsync();

            var data = _mapper.Map<IReadOnlyList<corndisease>, IReadOnlyList<CornDiseaseToReturnDto>>(corndiseases);


            var responseDto = new CornDiseaseResponseDto
            {
                Data = data
            };

            return Ok(responseDto);

            // return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CornDiseaseToReturnDto>> GetCornDisease(int id)
        {
            var cornDisease = await _unitOfWork.Repository<corndisease>().GetByIdAsync(id);

            if (cornDisease == null)
            {
                return NotFound(new ApiResponse(404));
            }

            var cornDiseaseDto = _mapper.Map<corndisease, CornDiseaseToReturnDto>(cornDisease);

            return Ok(cornDiseaseDto);
        }



    }
}

