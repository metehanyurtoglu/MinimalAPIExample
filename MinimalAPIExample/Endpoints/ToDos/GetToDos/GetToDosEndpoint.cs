using Carter;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalAPIExample.Core;
using MinimalAPIExample.Endpoints.ToDos.CreateToDo;
using MinimalAPIExample.Endpoints.ToDos.GetToDo;

namespace MinimalAPIExample.Endpoints.ToDos.GetToDos
{
    public record GetToDosRequest([FromQuery] int Page = 1, [FromQuery] int Size = 10);
    public record GetToDosResponse(List<GetToDoResponse> ToDos);

    public class GetToDosEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet($"/todoitems", async ([AsParameters] GetToDosRequest request, ApplicationContext context) =>
            {
                var skip = (request.Page - 1) * request.Size;

                var todos = await context.ToDos.Skip(skip).Take(request.Size).ToListAsync();

                return Results.Ok(new GetToDosResponse(todos.Adapt<List<GetToDoResponse>>()));
            })
            .WithName("GetToDos")
            .Produces<CreateToDoResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get ToDos")
            .WithDescription("Gets ToDos");
        }
    }
}