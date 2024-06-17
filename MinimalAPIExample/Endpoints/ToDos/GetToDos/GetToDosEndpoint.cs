
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MinimalAPIExample.Core;
using MinimalAPIExample.Endpoints.ToDos.GetToDo;

namespace MinimalAPIExample.Endpoints.ToDos.GetToDos
{
    public class GetToDosEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"/todoitems", async ([AsParameters] GetToDosRequest request, ApplicationContext context) =>
            {
                var skip = (request.Page - 1) * request.Size;

                var todos = await context.ToDos.Skip(skip).Take(request.Size).ToListAsync();

                return Results.Ok(new GetToDosResponse(todos.Adapt<List<GetToDoResponse>>()));
            });
        }
    }
}