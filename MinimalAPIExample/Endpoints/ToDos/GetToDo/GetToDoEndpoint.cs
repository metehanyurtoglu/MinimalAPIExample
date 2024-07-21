using Carter;
using Mapster;
using MinimalAPIExample.Core;
using MinimalAPIExample.Endpoints.ToDos.CreateToDo;

namespace MinimalAPIExample.Endpoints.ToDos.GetToDo
{
    public record GetToDoResponse(int Id, string ToDo, bool IsCompleted);

    public class GetToDoEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("todoitems/{id}", async (int id, ApplicationContext context) =>
            {
                var toDo = await context.ToDos.FindAsync(id);

                if (toDo is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(toDo.Adapt<GetToDoResponse>());
            })
            .WithName("GetToDo")
            .Produces<CreateToDoResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get ToDo")
            .WithDescription("Gets ToDo");
        }
    }
}
