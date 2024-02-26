using Microsoft.EntityFrameworkCore;
using PlantDiseaseX.Core.Entities;
using PlantDiseaseX.Repository.Data.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlantDiseaseX.Repository.Data
{
    public class PlantContext : DbContext
    {
        public PlantContext(DbContextOptions<PlantContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfiguration(new PlantConfigurations());
            //modelBuilder.ApplyConfiguration(new PlantCategoryConfigurations());
            //modelBuilder.ApplyConfiguration(new PlantSeasonConfigurations());

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


        }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<Plantcategory> PlantCategories { get; set; }
        public DbSet<Season> PlantSeasons { get; set; }


    }
}
