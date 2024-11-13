

global using backend.models;
using backend.Services.PropertyManagerServices;
using Microsoft.OpenApi.Models;
using backend.Services.PropertyServices;
using backend.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swaggerInfo =>
{
    swaggerInfo.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1.0.0",
        Title = "TvättTid",
        Description = "API Endpoints for laundry booking solutions."
    });
});
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IPropertyManagerService, PropertyManagerService>();
builder.Services.AddScoped<IPropertyServices, PropertyServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
