using AutoMapper;
using plantdiseasex.core.entities;
using PlantDiseaseX.API.Dtos;
using PlantDiseaseX.Core.Entities;

namespace PlantDiseaseX.API.Helpers
{
    public class CornDiseasePictureUrlResolver : IValueResolver<corndisease, CornDiseaseToReturnDto, string>
    {
       
        private readonly IConfiguration _configuration;

        public CornDiseasePictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(corndisease source, CornDiseaseToReturnDto destination, string desMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.corndiseasepicture1))
            {
                return $"{_configuration["ApiBaseUrl"]}{source.corndiseasepicture1}";

            }
            if (!string.IsNullOrEmpty(source.corndiseasepicture2))
            {
                return $"{_configuration["ApiBaseUrl"]}{source.corndiseasepicture2}";

            }
            if (!string.IsNullOrEmpty(source.corndiseasepicture2))
            {
                return $"{_configuration["ApiBaseUrl"]}{source.corndiseasepicture2}";

            }

            return string.Empty;
        }
    }
}
