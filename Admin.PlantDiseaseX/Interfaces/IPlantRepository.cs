using PlantDiseaseX.Core.Entities;
using PlantDiseaseX.Core.Repositories;

namespace Admin.PlantDiseaseX.Interfaces
{
    public interface IPlantRepository :IGenericRepository<Plant>
    {

        IQueryable<Plant> SearchPlantByName(string name);
       

    }
}
