using MedicinePlanner.Api.Extensions;
using MedicinePlanner.Api.Middlewares;
using MedicinePlanner.Data.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace MedicinePlanner.Api
{
    public class Startup
    {
        readonly string MedicinePlannerOrigins = "_medicinePlannerOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {            
            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")) );

            services.AddCors(options =>
            {
                options.AddPolicy(name: MedicinePlannerOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:4200", "http://localhost:3000")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();
                                  });
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MedicinePlanner.Api", Version = "v1" });
            });

            services.AddServicesExtension();
            services.AddMappingProfiles();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MedicinePlanner.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<CustomExceptionMiddleware>();

            app.UseRouting();
            app.UseCors(MedicinePlannerOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
