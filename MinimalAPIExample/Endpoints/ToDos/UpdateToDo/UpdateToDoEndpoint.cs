using Carter;
using Carter.ModelBinding;
using FluentValidation;
using Mapster;
using MinimalAPIExample.Core;
using MinimalAPIExample.Endpoints.ToDos.CreateToDo;

namespace MinimalAPIExample.Endpoints.ToDos.UpdateToDo
{
    public class UpdateToDoValidator : AbstractValidator<UpdateToDoRequest>
    {
        public UpdateToDoValidator()
        {
            RuleFor(request => request.ToDo)
                .NotEmpty().WithMessage("ToDo is required")
                .Length(2, 150).WithMessage("ToDo must be between 2 and 150 characters");
        }
    }

    public record UpdateToDoRequest(string ToDo, bool IsCompleted);

    public class UpdateToDoEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/todoitems/{id}", async (int id, UpdateToDoRequest request, UpdateToDoValidator validator, ApplicationContext context) =>
            {
                var result = validator.Validate(request);

                if (!result.IsValid)
                {
                    return Results.BadRequest(result.GetFormattedErrors());
                }

                var toDo = await context.ToDos.FindAsync(id);

                if (toDo is null)
                {
                    return Results.NotFound();
                }

                request.Adapt(toDo);

                await context.SaveChangesAsync();

                return Results.NoContent();
            })
            .WithName("UpdateToDo")
            .Produces<CreateToDoResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update ToDo")
            .WithDescription("Updates ToDos"); ;
        }
    }
}
