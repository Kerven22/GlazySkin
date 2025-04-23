using Domain.UseCases.CategoryUseCases.GetCategoryUseCase;
using Entity;
using Entity.Repositories.Category;

namespace GlazySkin.Extenttions
{
    public static class MyServices
    {
        public static void ConfigureSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSqlServer<GlazySkinDbContext>(configuration.GetConnectionString("default"), b => b.MigrationsAssembly(nameof(GlazySkin)));
        }

        public static void ConfigureUseCases(this IServiceCollection services) =>
            services.AddScoped<IGetCategoryUseCase, GetCategoryUseCase>()
            .AddScoped<IGetCategoryStorage, GetCategoryStorage>();


    }
}
