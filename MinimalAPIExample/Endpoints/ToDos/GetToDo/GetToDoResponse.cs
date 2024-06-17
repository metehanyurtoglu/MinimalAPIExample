namespace MinimalAPIExample.Endpoints.ToDos.GetToDo
{
    public record GetToDoResponse(int Id, string ToDo, bool IsCompleted);
}
