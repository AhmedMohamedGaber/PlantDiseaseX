using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using PlantDiseaseX.Core.Entities;
using PlantDiseaseX.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantDiseaseX.Repository
{
    public static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;

            if(spec.Criteria is not null)
                query = query.Where(spec.Criteria);





            if (spec.OrderBy is not null)    // P => P.Disease
            {
                query = query.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDescending is not null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            if(spec.IsPaginationEnable)
                query = query.Skip(spec.Skip).Take(spec.Take);



            query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));


            return query;
        }
    }
}
