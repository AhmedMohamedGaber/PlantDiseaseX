using Admin.PlantDiseaseX.Models;
using AutoMapper;
using plantdiseasex.core.entities;
using PlantDiseaseX.Core.Entities;

namespace Admin.PlantDiseaseX.Helper
{
    public class MapsProfile : Profile
    {
        public MapsProfile()
        {
            CreateMap<Plant, PlantViewModel>().ReverseMap();
            CreateMap<Plantcategory, CategoryViewModel>();
            CreateMap<Season, SeasonViewModel>();
            CreateMap<NewsArticle, NewsArticleViewModel>().ReverseMap();
            CreateMap<corndisease , CornDiseasecsViewModel>().ReverseMap();
            CreateMap<ContactUs,ContactUsViewModel>().ReverseMap();
        }
    }
}
