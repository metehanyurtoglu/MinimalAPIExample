using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using MinimalAPIExample;
using MinimalAPIExample.Core;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("ToDo"));
    builder.Services.AddHealthChecks();
    builder.Services.AddMapster();
    builder.Services.AddEndpoints();
}

var app = builder.Build();
{
    app.MapHealthChecks("/health");
    app.MapEndpoints();
    app.Run();
}