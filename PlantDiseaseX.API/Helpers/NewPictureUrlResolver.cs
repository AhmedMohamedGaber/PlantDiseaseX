using AutoMapper;
using PlantDiseaseX.API.Dtos;
using PlantDiseaseX.Core.Entities;

namespace PlantDiseaseX.API.Helpers
{
    public class NewPictureUrlResolver : IValueResolver<NewsArticle,NewToReturnDto , string>
    {
        private readonly IConfiguration _configuration;

       

        public NewPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(NewsArticle source, NewToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.NewsPicture))
            {
               
                return $"{_configuration["ApiBaseUrl"]}{source.NewsPicture}";
            }

            return string.Empty;
        }

        
    }

}
