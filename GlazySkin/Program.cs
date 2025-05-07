using Entity;
using GlazySkin.Extentions;
using GlazySkin.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger(); 

builder.Services.AddControllers().AddApplicationPart(typeof(Presentation.PresentationAssembly).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.CorsConfigure();
builder.Services.RepositoryManagerConfigure();
builder.Services.ServiceManagerConfigure(); 
builder.Services.AddDbContext<GlazySkinDbContext>();
builder.Services.AddAutoMapper(typeof(Program)); 
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