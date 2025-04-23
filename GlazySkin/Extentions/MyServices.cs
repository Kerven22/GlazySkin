using Entity;

namespace GlazySkin.Extenttions
{
    public static class MyServices
    {
        public static void ConfigureSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSqlServer<GlazySkinDbContext>(configuration.GetConnectionString("default"), b => b.MigrationsAssembly(nameof(GlazySkin))); 
        }
    }
}
