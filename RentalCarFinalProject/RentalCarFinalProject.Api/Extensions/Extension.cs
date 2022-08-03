using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using RentalCarFinalProject.Core;
using RentalCarFinalProject.Data;
using RentalCarFinalProject.Service.Exceptions;
using RentalCarFinalProject.Service.Implementations;
using RentalCarFinalProject.Service.Interfaces;

namespace RentalCarFinalProject.Api.Extensions
{
    public static class Extension
    {
        public static void AddScoppedService(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IColorService, ColorService>();
            services.AddScoped<IFuelService, FuelService>();
            services.AddScoped<IYearService, YearService>();
            services.AddScoped<IEngineService, EngineService>();
            services.AddScoped<ITransmissionService,TransmissionService>();
        }

        public static void ExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(
                error =>
                {

                    error.Run(async context =>
                    {
                        var feature = context.Features.Get<IExceptionHandlerPathFeature>();

                        int statusCode = 500;
                        string message = "Internal Server Error";

                        if (feature.Error is AlreadyExistsException)
                        {
                            statusCode = 400;
                            message = feature.Error.Message;
                        }
                        else if (feature.Error is BadRequestException)
                        {
                            statusCode = 415;
                            message = feature.Error.Message;
                        }
                        else if (feature.Error is NotFoundException)
                        {
                            statusCode = 404;
                            message = feature.Error.Message;

                        }
                        context.Response.StatusCode = statusCode;
                        await context.Response.WriteAsync(message);
                    });
                });
        }

    }
}
