using PlantDiseaseX.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlantDiseaseX.Repository.Data
{
    public static class PlantContextSeed
    {
        public static async Task SeedAsync(PlantContext dbContext)
        {
            if(!dbContext.PlantCategories.Any())
            {
                var CategoriesData = File.ReadAllText("../PlantDiseaseX.Repository/Data/DataSeed/Categories.json");
                var Categories = JsonSerializer.Deserialize<List<Plantcategory>>(CategoriesData);

                if (Categories?.Count > 0)
                {
                    foreach (var category in Categories)
                        await dbContext.Set<Plantcategory>().AddAsync(category);

                    await dbContext.SaveChangesAsync();
                }

            }

            if (!dbContext.PlantSeasons.Any())
            {
                var seasonsData = File.ReadAllText("../PlantDiseaseX.Repository/Data/DataSeed/Seasons.json");
                var seasons = JsonSerializer.Deserialize<List<Season>>(seasonsData);

                if (seasons?.Count > 0)
                {
                    foreach (var season in seasons)
                        await dbContext.Set<Season>().AddAsync(season);

                    await dbContext.SaveChangesAsync();
                }

            }

            if (!dbContext.Plants.Any())
            {
                var plantsData = File.ReadAllText("../PlantDiseaseX.Repository/Data/DataSeed/Plants.json");
                var plants = JsonSerializer.Deserialize<List<Plant>>(plantsData);

                if (plants?.Count > 0)
                {
                    foreach (var plant in plants)
                        await dbContext.Set<Plant>().AddAsync(plant);

                    await dbContext.SaveChangesAsync();
                }

            }

            //if (!dbContext.NewsArticles.Any())
            //{
            //    var newarticlesData = File.ReadAllText("../PlantDiseaseX.Repository/Data/DataSeed/NewsArticles.json");
            //    var newarticles = JsonSerializer.Deserialize<List<Plant>>(newarticlesData);

            //    if (newarticles?.Count > 0)
            //    {
            //        foreach (var newarticle in newarticles)
            //            await dbContext.Set<Plant>().AddAsync(newarticle);

            //        await dbContext.SaveChangesAsync();
            //    }

            //}
        }
    }
}
