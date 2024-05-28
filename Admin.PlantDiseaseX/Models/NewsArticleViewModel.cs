using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using PlantDiseaseX.Core.Entities;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Admin.PlantDiseaseX.Models
{
    public class NewsArticleViewModel
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Title is Required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is Required")]

        public string? Description { get; set; }


        public IFormFile? NewsImage { get; set; }
        public string? NewsPicture { get; set; }


        [Required(ErrorMessage = "Date is Required")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        //public string? FullNewsPictureUrl => !string.IsNullOrEmpty(NewsPicture) ? $"http://plantdiseasexapi.runasp.net/{NewsPicture}" : null;
    }



}
