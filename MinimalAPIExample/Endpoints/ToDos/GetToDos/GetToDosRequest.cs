using Microsoft.AspNetCore.Mvc;

namespace MinimalAPIExample.Endpoints.ToDos.GetToDos
{
    public record GetToDosRequest([FromQuery] int Page = 1, [FromQuery] int Size = 10);
}
