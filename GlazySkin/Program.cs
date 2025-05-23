using Entity;
using GlazySkin.ActionFilter;
using GlazySkin.Extentions;
using GlazySkin.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using RepositoryContracts;
using Serilog;
using Servicies.DataSaping;
using Shared;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger(); 

builder.Services.AddControllers(config =>
    {
        config.RespectBrowserAcceptHeader = true;
        config.ReturnHttpNotAcceptable = true; 
        config.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
    })
    .AddXmlDataContractSerializerFormatters()
    .AddApplicationPart(typeof(Presentation.PresentationAssembly).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.CorsConfigure();
builder.Services.RepositoryManagerConfigure();
builder.Services.ServiceManagerConfigure(); 
builder.Services.AddDbContext<GlazySkinDbContext>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<ValidationFilterAttrebute>();
builder.Services.AddScoped<IDataShaper<ProductDto>, DataShaper<ProductDto>>();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter()=>
new ServiceCollection().AddLogging().AddMvc().AddNewtonsoftJson()
    .Services.BuildServiceProvider().GetRequiredService<IOptions<MvcOptions>>().Value.InputFormatters
    .OfType<NewtonsoftJsonPatchInputFormatter>().First();