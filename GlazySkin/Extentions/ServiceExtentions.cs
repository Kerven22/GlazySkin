using AspNetCoreRateLimit;
using Entity;
using Entity.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using RepositoryContracts;
using ServiceContracts;
using Servicies;
using System.Text;

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

        public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSetting");
            var secretKey = Environment.GetEnvironmentVariable("SECRET");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
        }
    }
}
