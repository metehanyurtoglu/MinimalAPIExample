
using MinimalAPIExample.Core;
using MinimalAPIExample.Core.Entities;

namespace MinimalAPIExample.Endpoints.ToDos.DeleteToDo
{
    public class DeleteToDoEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("/todoitems/{id}", async (int id, ApplicationContext context) =>
            {
                if (await context.ToDos.FindAsync(id) is ToDoItem toDo)
                {
                    context.ToDos.Remove(toDo);
                    await context.SaveChangesAsync();

                    return Results.NoContent();
                }

                return Results.NotFound();
            });
        }
    }
}