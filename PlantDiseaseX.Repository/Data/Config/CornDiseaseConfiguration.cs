using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PlantDiseaseX.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using plantdiseasex.core.entities;

namespace PlantDiseaseX.Repository.Data.Config
{
    public class CornDiseaseConfiguration : IEntityTypeConfiguration<corndisease>
    {
        public void Configure(EntityTypeBuilder<corndisease> builder)
        {
            builder.Property(N => N.Id).IsRequired();
            builder.Property(N => N.name).IsRequired().HasMaxLength(100);
            builder.Property(N => N.corndiseasepicture1).IsRequired(false);
            builder.Property(N => N.corndiseasepicture2).IsRequired(false);
            builder.Property(N => N.corndiseasepicture3).IsRequired(false);
            builder.Property(N => N.appropriatetreatment).IsRequired();
            builder.Property(N => N.reasons).IsRequired();
            builder.Property(N => N.symptoms).IsRequired();
            builder.Property(N => N.prevention).IsRequired();
            builder.Property(N => N.management).IsRequired();
            builder.Property(N => N.riskfactors).IsRequired();
            builder.Property(N => N.relateddiseases).IsRequired();
            builder.Property(N => N.researchreferences).IsRequired();
            builder.Property(N => N.additionalinfo).IsRequired();
            builder.Property(N => N.diagnostictests).IsRequired();
            builder.Property(N => N.geographicdistribution).IsRequired();
            builder.Property(N => N.environmentalconditions).IsRequired();
            builder.Property(N => N.hostplants).IsRequired();
            builder.Property(N => N.pathogentype).IsRequired();
            builder.Property(N => N.economicimpact).IsRequired();
            builder.Property(N => N.controlmethods).IsRequired();

    }
    }
    
}
