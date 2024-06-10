using Microsoft.EntityFrameworkCore;
using TaskR.Model;

namespace TaskR.DB
{
    public class TaskrContext: DbContext
    {
        public TaskrContext(DbContextOptions<TaskrContext> options): base(options)
        {}

        public DbSet<Taskr> Tasks { get; set; }
    }
}
