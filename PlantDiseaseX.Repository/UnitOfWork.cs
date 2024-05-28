using Microsoft.EntityFrameworkCore;
using PlantDiseaseX.Core;
using PlantDiseaseX.Core.Entities;
using PlantDiseaseX.Core.Repositories;
using PlantDiseaseX.Repository.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantDiseaseX.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repositories;
        private readonly PlantContext _dbContext;

        public UnitOfWork(PlantContext dbContext)
        {
            _dbContext = dbContext;
            
        }
        public async Task<int> Complete()
        {
            return await _dbContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity>(_dbContext);
                _repositories.Add(type, repository);
            }

            return (IGenericRepository<TEntity>)_repositories[type];
        }

    }

 
}
