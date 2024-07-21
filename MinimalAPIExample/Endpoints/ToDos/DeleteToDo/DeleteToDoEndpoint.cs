using Carter;
using MinimalAPIExample.Core;
using MinimalAPIExample.Core.Entities;
using MinimalAPIExample.Endpoints.ToDos.CreateToDo;

namespace MinimalAPIExample.Endpoints.ToDos.DeleteToDo
{
    public class DeleteToDoEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
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
            })
            .WithName("DeleteToDo")
            .Produces<CreateToDoResponse>(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete ToDo")
            .WithDescription("Deletes ToDo");
        }
    }
}