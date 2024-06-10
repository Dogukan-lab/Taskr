using TaskR.Model;

namespace TaskR.Repository
{
    /// <summary>
    /// Interface used for communication between a controller and the actual repository
    /// This repository interface is used for CRUD operations inside of the database.
    /// </summary>
    public interface ITaskrRepository
    {
        bool AddTask(Taskr task);
        ICollection<Taskr> GetAllTasks();
        Taskr GetTask(int id);
        bool UpdateTask(Taskr task);
        bool DeleteTask(int id);

    }
}
