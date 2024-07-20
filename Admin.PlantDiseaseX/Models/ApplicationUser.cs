using Microsoft.AspNetCore.Identity;

namespace Admin.PlantDiseaseX.Models
{
    public class ApplicationUser :IdentityUser
    {
        public string FName { get; set; }

        public string LName { get; set; }

        public bool IsAgree { get; set; }

        public DateTime? LastSignInDate { get; set; }
    }
}
