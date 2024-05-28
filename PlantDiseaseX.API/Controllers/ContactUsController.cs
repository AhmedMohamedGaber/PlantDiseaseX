using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlantDiseaseX.API.Dtos;
using PlantDiseaseX.API.Errors;
using PlantDiseaseX.API.Helpers;
using PlantDiseaseX.Core;
using PlantDiseaseX.Core.Entities;
using PlantDiseaseX.Core.Specifications;

namespace PlantDiseaseX.API.Controllers 
{
    public class ContactUsController : APIBaseController
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ContactUsController(

            IUnitOfWork unitOfWork,
            IMapper mapper)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        

        [HttpPost]
        public async Task<ActionResult<ContactUsDto>> CreateContactUs([FromBody] ContactUsCreateDto contactusCreateDto)
        {
            // Check if the incoming DTO is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map the incoming DTO to the Plant entity
            var contactusToCreate = _mapper.Map<ContactUsCreateDto, ContactUs>(contactusCreateDto);

            // Add the new plant entity to the database
            _unitOfWork.Repository<ContactUs>().AddAsync(contactusToCreate);
            await _unitOfWork.Complete();

            // Map the created plant entity back to DTO for response
            var ContactUsDto = _mapper.Map<ContactUs, ContactUsDto>(contactusToCreate);

            // Return the newly created plant DTO
            return CreatedAtAction(nameof(GetContactUs), new { id = ContactUsDto.Id }, ContactUsDto);
        }


        [HttpGet]
        public async Task<ActionResult<Pagination<ContactUsDto>>> GetContacstUs()
        {
            //var spec = new PlantWithCategoryAndSeasonSpecification(specParams);


         


          //  var data = _mapper.Map<IReadOnlyList<NewsArticle>, IReadOnlyList<NewToReturnDto>>(newsarticles);


            var contactus = await _unitOfWork.Repository<ContactUs>().GetAllAsync();


            var data = _mapper.Map<IReadOnlyList<ContactUs>, IReadOnlyList<ContactUsDto>>(contactus);

            var responseDto = new ContactUsResponseDto
            {
                Data = data
            };

            return Ok(responseDto);

            //  var count = await _unitOfWork.Repository<ContactUs>().GetCountAsync();

          //  return Ok(new Pagination<ContactUsDto>(specParams.PageIndex, specParams.PageSize, data));

        }

        [ProducesResponseType(typeof(ContactUsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactUsDto>> GetContactUs(int id)
        {
            var contactus = await _unitOfWork.Repository<ContactUs>().GetByIdAsync(id);

            if (contactus is null) return NotFound(new ApiResponse(404));

            var contactUsDto = _mapper.Map<ContactUs, ContactUsDto>(contactus);

            return Ok(contactUsDto);
        }


    }
}
