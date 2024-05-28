using System.ComponentModel.DataAnnotations;

namespace Admin.PlantDiseaseX.Models
{
    public class CornDiseasecsViewModel
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Name is Required")]

        public string name { get; set; }

        public IFormFile? CorndiseaseImage1 { get; set; }
        public string? corndiseasepicture1 { get; set; }

        public IFormFile? CorndiseaseImage2 { get; set; }

        public string? corndiseasepicture2 { get; set; }

        public IFormFile? CorndiseaseImage3 { get; set; }

        public string? corndiseasepicture3 { get; set; }



        [Required(ErrorMessage = "Appropriatetreatment is Required")]

        public string? appropriatetreatment { get; set; }


        [Required(ErrorMessage = "Reasons is Required")]

        public string? reasons { get; set; }


        [Required(ErrorMessage = "Symptoms is Required")]

        public string? symptoms { get; set; }


        [Required(ErrorMessage = "Prevention is Required")]

        public string? prevention { get; set; }


        [Required(ErrorMessage = "Management is Required")]

        public string? management { get; set; }


        [Required(ErrorMessage = "Riskfactors is Required")]

        public string? riskfactors { get; set; }


        [Required(ErrorMessage = "Relateddiseases is Required")]

        public string? relateddiseases { get; set; }


        [Required(ErrorMessage = "Researchreferences is Required")]

        public string? researchreferences { get; set; }


        [Required(ErrorMessage = "Additionalinfo is Required")]

        public string? additionalinfo { get; set; }



        [Required(ErrorMessage = "Diagnostictests is Required")]

        public string? diagnostictests { get; set; }


        [Required(ErrorMessage = "Geographicdistribution is Required")]

        public string? geographicdistribution { get; set; }



        [Required(ErrorMessage = "Environmentalconditions is Required")]

        public string? environmentalconditions { get; set; }



        [Required(ErrorMessage = "Hostplants is Required")]

        public string? hostplants { get; set; }


        [Required(ErrorMessage = "Pathogentype is Required")]

        public string? pathogentype { get; set; }


        [Required(ErrorMessage = "Economicimpact is Required")]

        public string? economicimpact { get; set; }


        [Required(ErrorMessage = "Controlmethods is Required")]

        public string? controlmethods { get; set; }
    }
}
