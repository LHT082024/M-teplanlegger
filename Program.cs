using NSwag.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Møteplanlegger;
using Møteplanlegger.models;
using Møteplanlegger.Migrations;
using Møteplanlegger.controllers;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DbContextClass>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "Arasaka-MeetingPlanner";
    config.Title = "MeetingPlanner v1";
    config.Version = "V1";
});


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "Arasaka-MeetingPlanner";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/Arasaka-MeetingPlanner/swagger.json";
        config.DocExpansion = "list";
    });
}

//this line makes sure that the main page of the website is /swagger
app.MapGet("/", () => Results.Redirect("/swagger"));
app.MapControllers();

app.Run();

