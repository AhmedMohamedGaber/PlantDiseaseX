using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlantDiseaseX.API.Errors;
//using PlantDiseaseX.API.Extensions;
using PlantDiseaseX.API.Helpers;
using PlantDiseaseX.API.Middlewares;
using PlantDiseaseX.Core.Entities;
using PlantDiseaseX.Core.Repositories;
using PlantDiseaseX.Repository;
using PlantDiseaseX.Repository.Data;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Connections;
using StackExchange.Redis;
//using PlantDiseaseX.API.Extensions;

namespace PlantDiseaseX.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services

            // Add services to the container.


            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
          //  builder.Services.AddSwaggerGen();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PlantDiseasX.Api", Version = "v1" });
            });



            // builder.Services.AddSwaggerServices();

            builder.Services.AddDbContext<PlantContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Radis (cacheing)

            builder.Services.AddSingleton<IConnectionMultiplexer>(config =>
            {
                var configuration = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(configuration);    
            });

           // builder.Services.AddApplicationService();

            // ApplicationServicesExtension.AddApplicationServices(builder.Services);

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            builder.Services.AddAutoMapper(typeof(MappingProfiles));

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                                         .SelectMany(P => P.Value.Errors)
                                                         .Select(E => E.ErrorMessage)
                                                         .ToArray();

                    var validationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(validationErrorResponse);
                };
            });

           // builder.Services.AddApplicationServices();

            #endregion

            #region Update-Database during run

            var app = builder.Build();

            using var scope = app.Services.CreateScope(); // manual scope (Explicitly)

            var services = scope.ServiceProvider;


            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbContext = services.GetRequiredService<PlantContext>();  //Here,We Ask CLR for creating object from dbContext Explicitly
                await dbContext.Database.MigrateAsync();  // always Update-Database during run

                await PlantContextSeed.SeedAsync(dbContext);
            }
            catch (Exception ex)
            {
              var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex,"An Error during apply the migration");
            }


            #endregion


            #region Configure App

            app.UseMiddleware<ExceptionMiddleware>();


           

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                //app.UseSwaggerUI();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlantDiseasX.Api v1");
                });
            }

            app.UseStatusCodePagesWithRedirects("/errors/{0}");

            app.UseHttpsRedirection();


            

            //app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();


            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            #endregion

            app.Run();
        }
    }
}