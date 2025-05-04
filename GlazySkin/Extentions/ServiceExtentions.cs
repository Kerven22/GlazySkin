using Repositories;
using RepositoryContracts;
using ServiceContracts;
using Servicies;

namespace GlazySkin.Extentions
{
    public static class ServiceExtentions
    {
        public static void CorsConfigure(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

        public static void RepositoryManagerConfigure(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ServiceManagerConfigure(this IServiceCollection service) =>
            service.AddScoped<IServiceManager, ServiceManager>();
    }
}
