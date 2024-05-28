//using PlantDiseaseX.Core.Entities;
//using System;
//using System.Collections.Generic;
//using System.Data.Common;
//using System.Linq;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;

//namespace PlantDiseaseX.Repository.Data
//{
//    public static class NewArticleContextSeed 
//    {
//        public static async Task SeedAsync(NewArticleContext dbContext)
//        {
           

//            if (!dbContext.NewsArticles.Any())
//            {
//                var newarticlesData = File.ReadAllText("../PlantDiseaseX.Repository/Data/DataSeed/NewsArticles.json");
//                var newarticles = JsonSerializer.Deserialize<List<Plant>>(newarticlesData);

//                if (newarticles?.Count > 0)
//                {
//                    foreach (var newarticle in newarticles)
//                        await dbContext.Set<Plant>().AddAsync(newarticle);

//                    await dbContext.SaveChangesAsync();
//                }

//            }


//        }

//    }
//}
