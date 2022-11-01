using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RestFul.Controllers.V1;
using RestFul.Data;
using RestFul.Repository;
using RestFul.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IJogoService, JogoService>();
builder.Services.AddScoped<IJogoRepository, JogoSqlServerRepository>();
builder.Services.AddDbContext<MainContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("MainContext")));
#region CicloDeViida
builder.Services.AddSingleton<IExemploSingleton, ExemploCicloDeVida>();
builder.Services.AddScoped<IExemploScoped, ExemploCicloDeVida>();
builder.Services.AddTransient<IExemploTransient, ExemploCicloDeVida>();
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RestFul"));
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();



app.Run();
