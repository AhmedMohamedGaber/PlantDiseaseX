using System.ComponentModel.DataAnnotations;

namespace PlantDiseaseX.API.Dtos
{
    public class PlantUpdateDto
    {
        public string Name { get; set; }

        public string PictureUrl { get; set; }

        public string Description { get; set; }

        public string Season { get; set; }

        public string Diseases { get; set; }

        public string Treatment { get; set; }

        public string Properties { get; set; }

        public string GeneralUse { get; set; }

        public string MedicalUse { get; set; }

        public string Warnings { get; set; }

        public int? PlantCategoryId { get; set; }

        public int? PlantSeasonId { get; set; }
    }
}
