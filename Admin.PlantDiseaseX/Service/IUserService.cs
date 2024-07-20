using System.Threading.Tasks;
using Admin.PlantDiseaseX.Models;

namespace Admin.PlantDiseaseX.Services
{
    public interface IUserService
    {
        Task<List<ApplicationUser>> GetCurrentlyLoggedInUsersAsync();
    }
}
