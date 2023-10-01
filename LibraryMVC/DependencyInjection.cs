using Microsoft.EntityFrameworkCore;

namespace LibraryMVC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureEFCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options =>
                options.UseNpgsql(configuration.GetValue<string>("MyDatabaseConnection")));
            return services;
        }
    }
}