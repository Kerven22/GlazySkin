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
    }
}
