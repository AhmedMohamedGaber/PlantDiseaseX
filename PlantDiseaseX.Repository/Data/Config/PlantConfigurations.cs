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
    internal class PlantConfigurations : IEntityTypeConfiguration<Plant>
    {
        public void Configure(EntityTypeBuilder<Plant> builder)
        {
            builder.Property(P => P.Name).IsRequired().HasMaxLength(100);
            builder.Property(P => P.Description).IsRequired();
            builder.Property(P => P.PictureUrl).IsRequired();

            builder.HasOne(P => P.PlantCategory).WithMany()
                .HasForeignKey(P => P.PlantCategoryId);

            builder.HasOne(P => P.PlantSeason).WithMany()
                .HasForeignKey(P => P.PlantSeasonId);
        }
    }
   
}
