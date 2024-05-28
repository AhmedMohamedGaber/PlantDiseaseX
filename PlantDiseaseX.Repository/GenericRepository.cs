using Microsoft.EntityFrameworkCore;
using PlantDiseaseX.Core.Entities;
using PlantDiseaseX.Core.Repositories;
using PlantDiseaseX.Core.Specifications;
using PlantDiseaseX.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantDiseaseX.Repository
{
    public class GenericRepository<T> : IGenericRepository<T>  where T : BaseEntity 
    {
        private readonly PlantContext _dbContext;

        public GenericRepository(PlantContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(T entity)
             =>await _dbContext.Set<T>().AddAsync(entity);


        public void Delete(T entity)
            => _dbContext.Set<T>().Remove(entity);



        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Plant))
                return (IReadOnlyList<T>)await _dbContext.Set<Plant>().Include(P => P.PlantCategory).Include(P => P.PlantSeason).ToListAsync();
            //return (IReadOnlyList<T>)await _dbContext.Plants.OrderBy(P => P.Name).Include(P => P.PlantCategory).Include(P => P.PlantSeason).ToListAsync();

            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            // _dbContext.Plants.Where(P => P.Id == id).Include(P => P.PlantCategory).Include(P => P.PlantSeason).ToListAsync();
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<int> GetCountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public void Update(T entity)
           => _dbContext.Set<T>().Update(entity);



        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec);
        }
    }
}
