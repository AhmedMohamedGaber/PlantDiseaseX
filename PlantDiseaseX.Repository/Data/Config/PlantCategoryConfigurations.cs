using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlantDiseaseX.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantDiseaseX.Repository.Data.Config
{
    public class PlantCategoryConfigurations : IEntityTypeConfiguration<Plantcategory>
    {
        public void Configure(EntityTypeBuilder<Plantcategory> builder)
        {
            builder.Property(C => C.Name).IsRequired();
            builder.HasIndex(c => c.Name).IsUnique();
        }
    }

}
