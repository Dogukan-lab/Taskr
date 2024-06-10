
using TaskR.DB;

namespace TaskR.Repository
{
    public class TaskrRepository : ITaskrRepository
    {
        private readonly TaskrContext _taskrContext;
        TaskrRepository(TaskrContext taskrContext)
        {
            _taskrContext = taskrContext;
        }

        public bool AddTask(Task task)
        {
            var res =_taskrContext.Tasks.Find(task.Id);
            if (res != null)
            {
                return false;
            } else
            {
                _taskrContext.Add(task);
                return _taskrContext.SaveChanges() > 0;
            }
        }

        public bool DeleteTask(int id)
        {
            var task = _taskrContext.Tasks.FirstOrDefault(res => res.Id == id);
            _ = _taskrContext.Remove(task);
            return _taskrContext.SaveChangesAsync().GetAwaiter().GetResult() > 0;
        }

        public ICollection<Task> GetAllTasks()
        {
            return _taskrContext.Tasks.ToList();
        }

        public Task GetTask(int id)
        {
            var task = _taskrContext.Tasks.FirstOrDefault(res => res.Id == id);
            return task == null ? throw (new Exception("User not found!")) : task;
        }

        public bool UpdateTask(Task task)
        {
            _taskrContext.Update(task);
            return true;
        }
    }
}
