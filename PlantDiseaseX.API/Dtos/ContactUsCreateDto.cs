using System.ComponentModel.DataAnnotations;

namespace PlantDiseaseX.API.Dtos
{
    public class ContactUsCreateDto
    {
        [Required]
        public string Name { get; set; }


        [Required]
        public string Email { get; set; }

        [Required]

        public string Subject { get; set; }

        [Required]


        public string Message { get; set; }

    }
}
