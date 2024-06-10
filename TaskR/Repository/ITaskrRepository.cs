﻿namespace TaskR.Repository
{
    /// <summary>
    /// Interface used for communication between a controller and the actual repository
    /// This repository interface is used for CRUD operations inside of the database.
    /// </summary>
    public interface ITaskrRepository
    {
        bool AddTask(Task task);
        ICollection<Task> GetAllTasks();
        Task GetTask(int id);
        bool UpdateTask(Task task);
        bool DeleteTask(int id);

    }
}
