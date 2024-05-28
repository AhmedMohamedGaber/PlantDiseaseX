//using Microsoft.EntityFrameworkCore;
//using PlantDiseaseX.Core.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;

//namespace PlantDiseaseX.Repository.Data
//{
//    public class NewArticleContext : DbContext
//    {
      
//            public NewArticleContext(DbContextOptions<NewArticleContext> options) : base(options)
//            {

//            }

//            protected override void OnModelCreating(ModelBuilder modelBuilder)
//            {
//                base.OnModelCreating(modelBuilder);


//                modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


//            }
//            public DbSet<NewsArticle> NewsArticles { get; set; }
           


//        }
//    }

