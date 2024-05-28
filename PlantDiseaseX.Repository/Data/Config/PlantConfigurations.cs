using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlantDiseaseX.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace PlantDiseaseX.Repository.Data.Config
{
    public class PlantConfigurations : IEntityTypeConfiguration<Plant>
    {
        public void Configure(EntityTypeBuilder<Plant> builder)
        {
            builder.Property(P => P.Id).IsRequired();
            builder.Property(P => P.Name).IsRequired().HasMaxLength(100);
            builder.Property(P => P.Description).IsRequired();
            builder.Property(P => P.diseases).IsRequired();
            builder.Property(P => P.GeneralUse).IsRequired();
            builder.Property(P => P.season).IsRequired();
            builder.Property(P => P.Properties).IsRequired();
            builder.Property(P => P.Warnings).IsRequired();
            builder.Property(P => P.MedicalUse).IsRequired();
            builder.Property(P => P.treatment).IsRequired();
            builder.Property(P => P.PictureUrl).IsRequired(false);

            builder.HasOne(P => P.PlantCategory).WithMany()
                .HasForeignKey(P => P.PlantCategoryId);

            builder.HasOne(P => P.PlantSeason).WithMany()
                .HasForeignKey(P => P.PlantSeasonId);
        }
    }
   
}
