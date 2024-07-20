using AutoMapper;
using plantdiseasex.core.entities;
using PlantDiseaseX.API.Dtos;
using PlantDiseaseX.Core.Entities;

namespace PlantDiseaseX.API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Plant, PlantToReturnDto>()
                .ForMember(d => d.PlantCategory, O => O.MapFrom(s => s.PlantCategory.Name))
                .ForMember(d => d.PlantSeason , O =>O.MapFrom(s => s.PlantSeason.Name))
                .ForMember(d => d.PictureUrl, O => O.MapFrom<PlantPictureUrlResolver>());
            CreateMap<PlantCreateDto, Plant>();
            CreateMap<PlantUpdateDto, Plant>();

            CreateMap<NewsArticle, NewToReturnDto>()
                .ForMember(d => d.NewsPicture, O => O.MapFrom<NewPictureUrlResolver>());


            CreateMap<corndisease, CornDiseaseToReturnDto>()
                .ForMember(d => d.corndiseasepicture1, O => O.MapFrom<CornDiseasePicture1UrlResolver>())
                .ForMember(d => d.corndiseasepicture2, O => O.MapFrom<CornDiseasePicture2UrlResolver>())
                .ForMember(d => d.corndiseasepicture3, O => O.MapFrom<CornDiseasePicture3UrlResolver>());


            CreateMap<ContactUs, ContactUsDto>();
            CreateMap<ContactUsCreateDto ,ContactUs>();
               


        }
    }
}
