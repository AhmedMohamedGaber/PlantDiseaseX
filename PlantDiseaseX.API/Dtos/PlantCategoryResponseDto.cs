using PlantDiseaseX.Core.Entities;

namespace PlantDiseaseX.API.Dtos
{
    public class PlantCategoryResponseDto
    {
        public IReadOnlyList<Plantcategory> Data { get; set; }

    }
}
