using Admin.PlantDiseaseX.Controllers;
using Admin.PlantDiseaseX.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using NToastNotify;
using NuGet.Configuration;
using PlantDiseaseX.API.Helpers;
using PlantDiseaseX.Core;
using PlantDiseaseX.Core.Repositories;
using PlantDiseaseX.Identity;
using PlantDiseaseX.Repository;
using PlantDiseaseX.Repository.Data;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Admin.PlantDiseaseX.Models;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.Authentication.Cookies;
using Admin.PlantDiseaseX.Repository;
using Admin.PlantDiseaseX.Services;

namespace Admin.PlantDiseaseX
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add HttpClient
            builder.Services.AddHttpClient();

            // Add DbContext
            builder.Services.AddDbContext<PlantContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add Identity with PlantContext
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
            })
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            // Add Identity with AppIdentityDbContext
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            // Configure Authentication with Cookies
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Home/Error";
                });

            builder.Services.AddMvc().AddNToastNotifyToastr(new NToastNotify.ToastrOptions()
            {
                ProgressBar = true,
                PositionClass = ToastPositions.TopRight,
                PreventDuplicates = true,
                CloseButton = true
            });

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            builder.Services.AddAutoMapper(typeof(MapsProfile));

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var identityDbContext = services.GetRequiredService<AppIdentityDbContext>();
                    await identityDbContext.Database.MigrateAsync();
                }
                catch (Exception ex)
                {
                    // Handle the error appropriately
                    Console.WriteLine("An error occurred while migrating the database: " + ex.Message);
                }
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
