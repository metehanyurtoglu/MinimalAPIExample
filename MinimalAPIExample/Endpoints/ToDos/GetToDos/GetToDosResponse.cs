using MinimalAPIExample.Endpoints.ToDos.GetToDo;

namespace MinimalAPIExample.Endpoints.ToDos.GetToDos
{
    public record GetToDosResponse(List<GetToDoResponse> ToDos);
}
