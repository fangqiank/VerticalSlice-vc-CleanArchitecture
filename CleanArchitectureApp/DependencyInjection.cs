using CleanArchitectureApp.Common;
using CleanArchitectureApp.Data;
using CleanArchitectureApp.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CleanArchitectureApp
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            return services;
        }

        public static IServiceCollection AddInfrastructure(
         this IServiceCollection services,
         IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("TodoDB"));

            services.AddScoped<ITodoRepository, TodoRepository>();

            return services;
        }
    }
}
