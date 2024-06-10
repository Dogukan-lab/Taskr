using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskR.Model;
using TaskR.Repository;

namespace TaskR.Controllers
{
    /// <summary>
    /// Repository used as translation layer for SQL queries and 
    /// usable data models.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController: Controller
    {
        private readonly ITaskrRepository _taskrRepository;
        public TaskController(ITaskrRepository taskrRepository)
        {
            _taskrRepository = taskrRepository;
        }

        /// <summary>
        /// Gets all tasks inside of the database.
        /// </summary>
        /// <response code="200">Tasks have been found!</response>
        /// <response code="500">Application cant find tasks</response>
        [HttpGet("AllTasks")]
        [ProducesResponseType(typeof(Taskr),200)]
        public IActionResult GetAllTasks()
        {
            var tasks = _taskrRepository.GetAllTasks();
            if (tasks is null)
                throw (new Exception("No tasks found!"));

            return Ok(tasks);
        }

        /// <summary>
        /// Updates the task specified
        /// </summary>
        /// <param name="task">Task that gets updated</param>
        /// <response code="200">Tasks has been updated!</response>
        /// <response code="400">Task has not been updated!</response>
        [HttpPut("UpdateTask")]
        public IActionResult UpdateTask(Taskr task)
        {
            var  result = _taskrRepository.UpdateTask(task);
            
            return result ? Ok("Task has been updated!") : BadRequest("Task has not been updated!");
        }

        /// <summary>
        /// Adds the task specified
        /// </summary>
        /// <param name="task">Task that gets added</param>
        /// <response code="200">Tasks has been added!</response>
        /// <response code="400">Task has not been added!</response>
        [HttpPost("AddTask")]
        public IActionResult AddTask(Taskr task)
        {
            var result = _taskrRepository.AddTask(task);

            return result ? Ok("Task added to database!") : BadRequest("Task either wrong format or found within database!");
        }

        /// <summary>
        /// Deletes the task specified
        /// </summary>
        /// <param name="task">Task that has to be deleted</param>
        /// <response code="200">Tasks has been deleted!</response>
        /// <response code="400">Task has not been deleted!</response>
        [HttpDelete("DeleteTask")]
        public IActionResult DeleteTask(int taskId) 
        {
            var result = _taskrRepository.DeleteTask(taskId);
            return result ? Ok("Task deleted from db!") : BadRequest("Task cant be deleted!");
        }
    }
}
