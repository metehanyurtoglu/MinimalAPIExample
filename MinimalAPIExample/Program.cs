using Carter;
using Mapster;
using Microsoft.EntityFrameworkCore;
using MinimalAPIExample.Core;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("ToDo"));

    builder.Services.AddHealthChecks();
    builder.Services.AddCarter();
    builder.Services.AddMapster();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();
{
    app.MapHealthChecks("/health");
    app.MapCarter();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.Run();
}