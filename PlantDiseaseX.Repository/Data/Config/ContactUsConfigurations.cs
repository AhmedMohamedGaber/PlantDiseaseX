using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlantDiseaseX.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantDiseaseX.Repository.Data.Config
{
    public class ContactUsConfigurations
    {
        public void configure(EntityTypeBuilder<ContactUs> builder)
        {
            builder.Property(N => N.Id).IsRequired();
            builder.Property(N => N.Name).IsRequired().HasMaxLength(100);
            builder.Property(N => N.Email).IsRequired();
            builder.Property(N => N.Subject).IsRequired();
            builder.Property(N => N.Message).IsRequired();



        }
    }
}
