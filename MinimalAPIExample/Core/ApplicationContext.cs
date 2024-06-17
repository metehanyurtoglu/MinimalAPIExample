using Microsoft.EntityFrameworkCore;
using MinimalAPIExample.Core.Entities;

namespace MinimalAPIExample.Core
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            
        }

        public DbSet<ToDoItem> ToDos { get; set; }
    }
}
