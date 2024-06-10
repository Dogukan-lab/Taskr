using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("AllTasks")]
        public IActionResult GetAllUsers()
        {
            var tasks = _taskrRepository.GetAllTasks();
            if (tasks is null)
                throw (new Exception("No tasks found!"));

            return Ok(tasks);
        }

        [HttpPut("UpdateTask")]
        public IActionResult UpdateTask(Task task)
        {
            var  result = _taskrRepository.UpdateTask(task);
            
            return result ? Ok("Task has been updated!") : BadRequest("Task has not been updated!");
        }

        [HttpPost("AddTask")]
        public IActionResult AddTask(Task task)
        {
            var result = _taskrRepository.AddTask(task);

            return result ? Ok("Task added to database!") : BadRequest("Task either wrong format or found within database!");
        }

        [HttpDelete("DeleteTask")]
        public IActionResult DeleteTask(int taskId) 
        {
            var result = _taskrRepository.DeleteTask(taskId);
            return result ? Ok("Task deleted from db!") : BadRequest("Task cant be deleted!");
        }
    }
}
