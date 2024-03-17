using PlantDiseaseX.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantDiseaseX.Core.Specifications
{
    public class PlantWithFilterationForCountSpecification : BaseSpecification<Plant>
    {
        public PlantWithFilterationForCountSpecification(PlantSpecParams specParams)
             : base(P =>
                     (string.IsNullOrEmpty(specParams.Search) || P.Name.ToLower().Contains(specParams.Search)) &&
                     (!specParams.CategoryId.HasValue || P.PlantCategoryId == specParams.CategoryId.Value) &&
                     (!specParams.SeasonId.HasValue || P.PlantSeasonId == specParams.SeasonId.Value)
            )
        {

        }


    }
}
