using Microsoft.EntityFrameworkCore;
using PlantDiseaseX.Repository.Data;

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
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<PlantContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

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

            }
            catch (Exception ex)
            {
              var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex,"An Error during apply the migration");
            }

            #endregion


            #region Configure App

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers(); 




            #endregion

            app.Run();
        }
    }
}