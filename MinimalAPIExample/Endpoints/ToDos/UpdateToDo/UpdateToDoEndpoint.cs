
using Carter;
using Mapster;
using MinimalAPIExample.Core;

namespace MinimalAPIExample.Endpoints.ToDos.UpdateToDo
{
    public record UpdateToDoRequest(string ToDo, bool IsCompleted);

    public class UpdateToDoEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/todoitems/{id}", async (int id, UpdateToDoRequest request, ApplicationContext context) =>
            {
                var toDo = await context.ToDos.FindAsync(id);

                if (toDo is null)
                {
                    return Results.NotFound();
                }

                request.Adapt(toDo);

                await context.SaveChangesAsync();

                return Results.NoContent();
            });
        }
    }
}
