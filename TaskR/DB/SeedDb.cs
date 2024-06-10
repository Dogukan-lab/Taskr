using TaskR.error;
using TaskR.Model;

namespace TaskR.error
{
    enum RUNTIME 
    {
        SUCCESS = 0,
        FAILURE = -1,
        TASKS_EXIST = -2,
        DB_CONTEXT_NULL = -3,
    }
}
namespace TaskR.DB
{
    /// <summary>
    /// Function to seed the database with arbitrary data.
    /// </summary>
    public class SeedDb
    {
        public static void SeedTaskrDB(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetService<TaskrContext>();

            SeedInternal(dbContext);

        }

        public static void SeedInternal(TaskrContext dbContext)
        {
            if(dbContext is null)
            {
                throw (new Exception($"Exited seed database with: {RUNTIME.DB_CONTEXT_NULL}"));
            }
            if(dbContext.Tasks.Any())
            {
                throw (new Exception($"Exited seed database with: {RUNTIME.TASKS_EXIST}"));
            }

            var taskOne = new Taskr
            {
                Name = "Get chores done",
                Description = "Chores have been delayed, fix this rq."
            };

            var taskTwo = new Taskr
            {
                Name = "Code for the Docker container",
                Description = "Docker container not yet setup, do it now!"
            };

            var taskThree = new Taskr
            {
                Name = "Setup DB Docker container",
                Description = "Mariadb container not yet setup!"
            };


            dbContext.AddRange(new List<Taskr> { taskOne, taskTwo, taskThree });
            dbContext.SaveChanges();
        }
    }
}
