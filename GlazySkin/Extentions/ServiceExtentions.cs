using AspNetCoreRateLimit;
using Entity;
using Entity.Models;
using Microsoft.AspNetCore.Identity;
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
                .AllowAnyHeader()
                .WithExposedHeaders("X-Pagination"));
            });

        public static void RepositoryManagerConfigure(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ServiceManagerConfigure(this IServiceCollection service) =>
            service.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigureRateLimitingOptions(this IServiceCollection service)
        {
            var rateLimitRules = new List<RateLimitRule>
            {
                new RateLimitRule
                {
                    Endpoint = "*", Limit = 10, Period = "5m"
                }
            };

            service.Configure<IpRateLimitOptions>(op =>
            {
                op.GeneralRules = rateLimitRules;
            });

            service.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            service.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>(); 
            service.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>(); 
            service.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>(); 
        }

        public static void ConfigurationIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, IdentityRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 10;
                o.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<GlazySkinDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}
