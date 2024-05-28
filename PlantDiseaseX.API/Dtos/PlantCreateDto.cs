using System.ComponentModel.DataAnnotations;

namespace PlantDiseaseX.API.Dtos
{
    public class PlantCreateDto
    {
        [Required]
        public string Name { get; set; }

        public string PictureUrl { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Season { get; set; }

        [Required]
        public string Diseases { get; set; }

        public string Treatment { get; set; }

        public string Properties { get; set; }

        public string GeneralUse { get; set; }

        public string MedicalUse { get; set; }

        public string Warnings { get; set; }

        [Required]
        public int PlantCategoryId { get; set; }

        [Required]
        public int PlantSeasonId { get; set; }
    }
}
