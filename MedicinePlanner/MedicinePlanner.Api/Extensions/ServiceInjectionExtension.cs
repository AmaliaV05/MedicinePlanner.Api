using MedicinePlanner.BusinessLogic.Interfaces;
using MedicinePlanner.BusinessLogic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MedicinePlanner.Api.Extensions
{
    public static class ServiceInjectionExtension
    {
        public static IServiceCollection AddServicesExtension(this IServiceCollection services)
        {
            services.AddScoped<IMedicineService, MedicineService>();           

            return services;
        }
    }
}
