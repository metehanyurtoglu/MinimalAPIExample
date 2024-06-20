using Carter;
using Mapster;
using MinimalAPIExample.Core;
using MinimalAPIExample.Core.Entities;
using MinimalAPIExample.Endpoints.ToDos.CreateToDo;

namespace MinimalAPIExample.Endpoints.ToDos.CreateToDo
{
    public record CreateToDoRequest(string ToDo);
    public record CreateToDoResponse(int Id);


    public class CreateToDoEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/todoitems", async (CreateToDoRequest request, ApplicationContext context) =>
            {
                var newToDoItem = request.Adapt<ToDoItem>();

                context.ToDos.Add(newToDoItem);

                await context.SaveChangesAsync();

                var response = newToDoItem.Adapt<CreateToDoResponse>();

                return Results.Created($"/todoitems/{response.Id}", response);
            });
        }
    }
}
