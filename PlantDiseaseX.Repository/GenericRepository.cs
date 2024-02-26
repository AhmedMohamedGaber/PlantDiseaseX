using Microsoft.EntityFrameworkCore;
using PlantDiseaseX.Core.Entities;
using PlantDiseaseX.Core.Repositories;
using PlantDiseaseX.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantDiseaseX.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly PlantContext _dbContext;

        public GenericRepository(PlantContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if(typeof(T) == typeof(Plant))
                return (IEnumerable<T>) await _dbContext.Plants.Include(P => P.PlantCategory).Include(P => P.PlantSeason).ToListAsync();
           return await _dbContext.Set<T>().ToListAsync();
        }

      

        public async Task<T> GetByIdAsync(int id)
        {

            return await _dbContext.Set<T>().FindAsync(id);
        }

       
    }
}
