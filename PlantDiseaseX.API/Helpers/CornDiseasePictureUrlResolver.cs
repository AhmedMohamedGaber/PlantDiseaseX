//using AutoMapper;
//using plantdiseasex.core.entities;
//using PlantDiseaseX.API.Dtos;
//using PlantDiseaseX.Core.Entities;

//namespace PlantDiseaseX.API.Helpers
//{
//    public class CornDiseasePictureUrlResolver : IValueResolver<corndisease, CornDiseaseToReturnDto, string>
//    {

//        private readonly IConfiguration _configuration;

//        public CornDiseasePictureUrlResolver(IConfiguration configuration)
//        {
//            _configuration = configuration;
//        }

//        public string Resolve(corndisease source, CornDiseaseToReturnDto destination, string desMember, ResolutionContext context)
//        {
//            if (!string.IsNullOrEmpty(source.corndiseasepicture1))
//            {
//                return $"{_configuration["ApiBaseUrl"]}{source.corndiseasepicture1}";

//            }
//            if (!string.IsNullOrEmpty(source.corndiseasepicture2))
//            {
//                return $"{_configuration["ApiBaseUrl"]}{source.corndiseasepicture2}";

//            }
//            if (!string.IsNullOrEmpty(source.corndiseasepicture3))
//            {
//                return $"{_configuration["ApiBaseUrl"]}{source.corndiseasepicture3}";

//            }

//            return string.Empty;
//        }
//    }
//}
using AutoMapper;
using plantdiseasex.core.entities;
using PlantDiseaseX.API.Dtos;
using PlantDiseaseX.Core.Entities;

namespace PlantDiseaseX.API.Helpers
{
    public class CornDiseasePicture1UrlResolver : IValueResolver<corndisease, CornDiseaseToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public CornDiseasePicture1UrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(corndisease source, CornDiseaseToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.corndiseasepicture1))
            {
                return $"{_configuration["ApiBaseUrl"]}{source.corndiseasepicture1}";
            }
            return string.Empty;
        }
    }

    public class CornDiseasePicture2UrlResolver : IValueResolver<corndisease, CornDiseaseToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public CornDiseasePicture2UrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(corndisease source, CornDiseaseToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.corndiseasepicture2))
            {
                return $"{_configuration["ApiBaseUrl"]}{source.corndiseasepicture2}";
            }
            return string.Empty;
        }
    }

    public class CornDiseasePicture3UrlResolver : IValueResolver<corndisease, CornDiseaseToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public CornDiseasePicture3UrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(corndisease source, CornDiseaseToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.corndiseasepicture3))
            {
                return $"{_configuration["ApiBaseUrl"]}{source.corndiseasepicture3}";
            }
            return string.Empty;
        }
    }
}
