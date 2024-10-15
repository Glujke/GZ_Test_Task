using GZ_Test_Repo.Entity;
using GZ_Test_Repo.Repository;
using GZ_Test_Repo.Context;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GZ Test API", Version = "v1" });
});

builder.Services.AddDbContext<GZ_DbContext>(options =>
    options.UseSqlite("Data Source=GZ_Test.db"));

builder.Services.AddScoped<IRepository<Area>, RepositoryBase<Area>>();
builder.Services.AddScoped<IRepository<Specialization>, RepositoryBase<Specialization>>();
builder.Services.AddScoped<IRepository<Cabinet>, RepositoryBase<Cabinet>>();
builder.Services.AddScoped<IRepository<Gender>, RepositoryBase<Gender>>();

builder.Services.AddScoped<IRepository<Doctor>, RepositoryDoctor>();
builder.Services.AddScoped<IRepository<Patient>, RepositoryPatient>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GZ Test API V1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();