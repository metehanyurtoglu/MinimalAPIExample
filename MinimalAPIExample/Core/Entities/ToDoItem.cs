namespace MinimalAPIExample.Core.Entities
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public required string ToDo { get; set; }
        public bool IsCompleted { get; set; }
    }
}