using AutoMapper;
using PlantDiseaseX.API.Dtos;
using PlantDiseaseX.Core.Entities;

namespace PlantDiseaseX.API.Helpers
{
    public class PlantPictureUrlResolver : IValueResolver<Plant, PlantToReturnDto, string>
    {

        private readonly IConfiguration _configuration;
        public PlantPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Plant source, PlantToReturnDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_configuration["ApiBaseUrl"]}{source.PictureUrl}";

                
            }
            return string.Empty ;
        }
    }
}
