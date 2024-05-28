using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PlantDiseaseX.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace PlantDiseaseX.Repository.Data.Config
{
    public class NewsArticleConfigurations : IEntityTypeConfiguration<NewsArticle>
    {
        public void Configure(EntityTypeBuilder<NewsArticle> builder)
        {
            builder.Property(N => N.Id).IsRequired();
            builder.Property(N => N.Title).IsRequired().HasMaxLength(100);
            builder.Property(N => N.NewsPicture).IsRequired(false);
            builder.Property(N => N.Description).IsRequired();
            builder.Property(N => N.Date).IsRequired();

        }
    }

  
}
