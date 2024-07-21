using Carter;
using Carter.ModelBinding;
using FluentValidation;
using Mapster;
using MinimalAPIExample.Core;
using MinimalAPIExample.Core.Entities;

namespace MinimalAPIExample.Endpoints.ToDos.CreateToDo
{
    public class CreateToDoValidator : AbstractValidator<CreateToDoRequest>
    {
        public CreateToDoValidator()
        {
            RuleFor(request => request.ToDo)
                .NotEmpty().WithMessage("ToDo is required")
                .Length(2, 150).WithMessage("ToDo must be between 2 and 150 characters");
        }
    }

    public record CreateToDoRequest(string ToDo);
    public record CreateToDoResponse(int Id);

    public class CreateToDoEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/todoitems", async (CreateToDoRequest request, CreateToDoValidator validator, ApplicationContext context) =>
            {
                var result = validator.Validate(request);

                if (!result.IsValid)
                {
                    return Results.BadRequest(result.GetFormattedErrors());
                }

                var newToDoItem = request.Adapt<ToDoItem>();

                context.ToDos.Add(newToDoItem);

                await context.SaveChangesAsync();

                var response = newToDoItem.Adapt<CreateToDoResponse>();

                return Results.Created($"/todoitems/{response.Id}", response);
            })
            .WithName("CreateToDo")
            .Produces<CreateToDoResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create ToDo")
            .WithDescription("Creates ToDo");
        }
    }
}
