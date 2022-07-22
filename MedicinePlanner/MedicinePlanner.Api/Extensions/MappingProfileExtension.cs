using MedicinePlanner.Api.Helpers.MappingProfile;
using Microsoft.Extensions.DependencyInjection;

namespace MedicinePlanner.Api.Extensions
{
    public static class MappingProfileExtension
    {
        public static IServiceCollection AddMappingProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingMedicine));
            services.AddAutoMapper(typeof(MappingStock));

            return services;
        }
    }
}
