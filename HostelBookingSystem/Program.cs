using HostelBookingSystem.DataAccess;
using HostelBookingSystem.HelpersClassLib;
using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DEPENDENCY INJECTION
// First we inject the DbContext from the DepdendencyInjectionHelper class
DependencyInjectionHelper.InjectDbContext(builder.Services);
// Here we make our program aware of our repositories
DependencyInjectionHelper.InjectRepositories(builder.Services);
// Calling our injected services
DependencyInjectionHelper.InjectServices(builder.Services);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthorization();

app.MapControllers();

app.Run();
