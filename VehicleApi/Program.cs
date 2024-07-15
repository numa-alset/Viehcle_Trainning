using Microsoft.EntityFrameworkCore;
using VehicleApi.Persistance;
using AutoMapper;
using VehicleApi.Mapping;
using VehicleApi.Core;
using VehicleApi.Core.Models;
using Microsoft.Extensions.FileProviders;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IPhotoRepository, PhotoRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddControllers();
builder.Services.Configure<PhotoSettings>(builder.Configuration.GetSection("PhotoSettings"));
builder.Services.AddDbContext<VehicleApiDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
 
//jhjhvjhv

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "photos")),
    RequestPath= new PathString("/photos")

}) ;

app.UseAuthorization();

app.MapControllers();

app.Run();

