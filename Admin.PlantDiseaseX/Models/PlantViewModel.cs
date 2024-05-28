using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using PlantDiseaseX.Core.Entities;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Admin.PlantDiseaseX.Models
{
    public class PlantViewModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        //  [StringLength(250)]
        public string Name { get; set; }





        //[Display(Name = "Select Plant Image....")]
        // [ValidateNever]
        public IFormFile? Image { get; set; }

        // [ValidateNever]
        public string? PictureUrl { get; set; }





        [Required(ErrorMessage = "Description is Required")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "season is Required")]
        public string? season { get; set; }


        [Required(ErrorMessage = "diseases is Required")]
        public string? diseases { get; set; }

        [Required(ErrorMessage = "treatment is Required")]
        public string? treatment { get; set; }
        [Required(ErrorMessage = "Properties is Required")]
        public string? Properties { get; set; }
        [Required(ErrorMessage = "GeneralUse is Required")]
        public string? GeneralUse { get; set; }
        [Required(ErrorMessage = "MedicalUse is Required")]
        public string? MedicalUse { get; set; }
        [Required(ErrorMessage = "Warnings  is Required")]
        public string? Warnings { get; set; }

        [Required(ErrorMessage = "PlantCategory is Required")]

        // [ForeignKey("PlantCategory")]
        public int PlantCategoryId { get; set; } // Foreign Key

        // النبات الواحد لديه قسم واحد فقط ولكن القسم لديه اكثر من نبات
        public Plantcategory? PlantCategory { get; set; }  // One



        [Required(ErrorMessage = "PlantSeason is Required")]
        // [ForeignKey("Season")]
        public int PlantSeasonId { get; set; } // Foreign Key
        public Season? PlantSeason { get; set; } // One

      

    }
}