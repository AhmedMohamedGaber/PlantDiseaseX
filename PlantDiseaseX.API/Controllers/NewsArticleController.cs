using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlantDiseaseX.API.Dtos;
using PlantDiseaseX.API.Errors;
using PlantDiseaseX.API.Helpers;
using PlantDiseaseX.Core.Entities;
using PlantDiseaseX.Core.Specifications;
using PlantDiseaseX.Core;
using System.Numerics;

namespace PlantDiseaseX.API.Controllers
{
    public class NewsArticleController : APIBaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public NewsArticleController(

          IUnitOfWork unitOfWork,
          IMapper mapper)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]   
        public async Task<ActionResult<IReadOnlyList<NewToReturnDto>>> GetNewsArticleS([FromQuery] PlantSpecParams specParams)
        {
          
            var newsarticles = await _unitOfWork.Repository<NewsArticle>().GetAllAsync();

           
            var data = _mapper.Map<IReadOnlyList<NewsArticle>, IReadOnlyList<NewToReturnDto>>(newsarticles);

            // var count = await _unitOfWork.Repository<NewsArticle>().GetAllAsync();


            var responseDto = new NewsArticleResponseDto
            {
                Data = data
            };

            return Ok(responseDto);

            // return Ok(data);
            // return Ok(newsarticles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NewToReturnDto>> GetNewsArticle(int id)
        {
            var newsArticle = await _unitOfWork.Repository<NewsArticle>().GetByIdAsync(id);

            if (newsArticle == null)
            {
                return NotFound(new ApiResponse(404));
            }

            var newsArticleDto = _mapper.Map<NewsArticle, NewToReturnDto>(newsArticle);

            return Ok(newsArticleDto);
        }

    }
}
