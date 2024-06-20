
using Carter;
using Mapster;
using MinimalAPIExample.Core;

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
            });
        }
    }
}
