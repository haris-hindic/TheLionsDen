using Microsoft.EntityFrameworkCore;
using TheLionsDen.Services;
using TheLionsDen.Services.Database;
using TheLionsDen.Services.Impl;
using TheLionsDen.Services.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IAmenityService, AmenityService>();

builder.Services.AddDbContext<TheLionsDenContext>(opts => opts.UseSqlServer(builder.Configuration.GetConnectionString("AppDb")));

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

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
