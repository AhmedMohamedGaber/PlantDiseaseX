using AutoMapper;
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
        }
    }
}
