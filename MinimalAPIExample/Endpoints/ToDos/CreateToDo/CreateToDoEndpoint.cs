using Mapster;
using MinimalAPIExample.Core;
using MinimalAPIExample.Core.Entities;
using MinimalAPIExample.Endpoints.ToDos.CreateToDo;

namespace MinimalAPIExample.Endpoints.ToDos.CreateToDo
{
    public class CreateToDoEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
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
