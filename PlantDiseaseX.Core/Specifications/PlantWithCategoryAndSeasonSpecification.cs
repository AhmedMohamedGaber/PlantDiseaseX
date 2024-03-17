using PlantDiseaseX.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantDiseaseX.Core.Specifications
{
    public class PlantWithCategoryAndSeasonSpecification : BaseSpecification<Plant>
    {

        // This Constractor is used for get all plants
        public PlantWithCategoryAndSeasonSpecification(PlantSpecParams specParams)
            : base(P =>
                     (string.IsNullOrEmpty(specParams.Search) || P.Name.ToLower().Contains(specParams.Search)) &&
                     (!specParams.CategoryId.HasValue || P.PlantCategoryId == specParams.CategoryId.Value) &&
                     (!specParams.SeasonId.HasValue || P.PlantSeasonId == specParams.SeasonId.Value)
            )
        {
            Includes.Add(P => P.PlantCategory);
            Includes.Add(P => P.PlantSeason);
            AddOrderBy(P => P.Name);

            ApplyPagination(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);


            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch(specParams.Sort)
                {
                    case "diseasesAsc":
                        AddOrderBy(P => P.diseases);
                        break;
                    case "diseasesDesc":
                        AddOrderByDescending(P => P.diseases);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }


            // default for example
            // totalPlants = 100
            // pageSize    = 10
            // pageIndex   =3 


        }


        // This Constractor is used for get a specific plant
        public PlantWithCategoryAndSeasonSpecification(int id ):base(P => P.Id == id)
        {
            Includes.Add(P => P.PlantCategory);
            Includes.Add(P => P.PlantSeason);
        }

    }
}
