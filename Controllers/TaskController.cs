using Microsoft.AspNetCore.Mvc;
using TaskApi.Models;

namespace TaskApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private static List<TaskItem> tasks = new();

    [HttpGet]
    public IActionResult GetTasks()
    {
        return Ok(tasks);
    }

    [HttpPost]
    public IActionResult CreateTask(TaskItem task)
    {
        task.Id = tasks.Count + 1;
        tasks.Add(task);

        return Ok(task);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTask(int id)
    {
        var task = tasks.FirstOrDefault(t =>t.Id == id);
        if (task == null)
        {
            return NotFound();
        }

        tasks.Remove(task);
        return NoContent();
    }
}