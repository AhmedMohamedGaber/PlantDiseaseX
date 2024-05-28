using Microsoft.AspNetCore.Mvc;
using PlantDiseaseX.API.Helpers;
using PlantDiseaseX.Core.Repositories;
using PlantDiseaseX.Core;
//using PlantDiseaseX.Core.Services;
using PlantDiseaseX.Repository;
using PlantDiseaseX.API.Errors;
//using PlantDiseaseX.Service;
using Microsoft.Extensions;
using Microsoft.Extensions.DependencyInjection;


namespace PlantDiseaseX.API.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
           

            services.AddScoped<IUnitOfWork, UnitOfWork>();

           
            //services.AddScoped<IGenericRepository<>, GenericRepository<>>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


            //services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));
            services.AddAutoMapper(typeof(MappingProfiles));


            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState.Where(M => M.Value.Errors.Count() > 0)
                                                         .SelectMany(M => M.Value.Errors)
                                                         .Select(E => E.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });
            return services;
        }
    }
}
