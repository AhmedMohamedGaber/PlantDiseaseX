using PlantDiseaseX.Core.Entities;

namespace PlantDiseaseX.API.Dtos
{
    public class PlantToReturnDto
    {
       
        public int Id { get; set; }
        public string Name { get; set; }

        public string PictureUrl { get; set; }

        public string Description { get; set; }

        public string season { get; set; }

        public string diseases { get; set; }

        public string treatment { get; set; }

        public string Properties { get; set; }

        public string GeneralUse { get; set; }

        public string MedicalUse { get; set; }

        public string Warnings { get; set; }


        public int PlantCategoryId { get; set; } 

        public string PlantCategory { get; set; }  

        public int PlantSeasonId { get; set; } 
        public string PlantSeason { get; set; } 

    }
}
