using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using NUnit.Framework.Internal;
using TaskR.Controllers;
using TaskR.DB;
using TaskR.Repository;
using TaskrIntegration;

public class TaskControllerTest
{
    private TaskrRepository _repository;
    private TaskController _controller;

    private readonly DbContextOptions<TaskrContext> _contextOptions = new
        DbContextOptionsBuilder<TaskrContext>().UseInMemoryDatabase("Taskr_mem").Options;
    private TaskrContext rContext = null!;

    [SetUp]
    public void Setup()
    {
        rContext = new TaskrContext(_contextOptions);

        _repository = new TaskrRepository(rContext);
        _controller = new TaskController(_repository);
        
        SeedDb.SeedInternal(rContext);
    }

    [TearDown]
    public void TearDown()
    {
        rContext.Database.EnsureDeleted();
        rContext.Dispose();
    }
    
    //Tests
    [Test]
    public void CreateTask()
    {
        //Happy flow
        var task = new Taskr
        {
            Name = "Docker has to work!",
            Description = "Docker has not yet worked! Make this something available"
        };
        
        var firstRes = _controller.AddTask(task);
        TestUtil.SafetyChecks(firstRes);
        
        //Unhappy flow
        Taskr task1 = null!;
        var badRes = _controller.AddTask(task1);
        TestUtil.SafetyChecks<BadRequestObjectResult>(badRes);
    }
}