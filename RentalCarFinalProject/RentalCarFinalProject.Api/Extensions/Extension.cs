using Microsoft.Extensions.DependencyInjection;
using RentalCarFinalProject.Core;
using RentalCarFinalProject.Data;

namespace RentalCarFinalProject.Api.Extensions
{
    public static class Extension
    {
        public static void AddScoppedService(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }


    }
}
