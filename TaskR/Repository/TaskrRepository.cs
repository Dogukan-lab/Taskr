
using TaskR.DB;
using TaskR.Model;

namespace TaskR.Repository
{
    public class TaskrRepository(TaskrContext taskrContext) : ITaskrRepository
    {
        public bool AddTask(Taskr? task)
        {
            if (task == null)
                return false;
            
            var res = taskrContext.Tasks.Find(task.Id);
            if (res != null)
            {
                return false;
            }

            taskrContext.Add(task);
            return taskrContext.SaveChanges() > 0;
        }

        public bool DeleteTask(int id)
        {
            var task = taskrContext.Tasks.FirstOrDefault(res => res.Id == id);
            _ = taskrContext.Remove(task);
            return taskrContext.SaveChangesAsync().GetAwaiter().GetResult() > 0;
        }

        public ICollection<Taskr> GetAllTasks()
        {
            return taskrContext.Tasks.ToList();
        }

        public Taskr GetTask(int id)
        {
            var task = taskrContext.Tasks.FirstOrDefault(res => res.Id == id);
            return task == null ? throw (new Exception("User not found!")) : task;
        }

        public bool UpdateTask(Taskr task)
        {
            taskrContext.Update(task);
            return true;
        }
    }
}
