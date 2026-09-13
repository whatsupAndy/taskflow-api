using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskApi.Data;
using TaskApi.Models;
using TaskApi.Dtos;
namespace TaskApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly AppDbContext _context;

    public TasksController(AppDbContext context)
    {
        _context = context;
    }


    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        var tasks = await _context.Tasks.ToListAsync();
        var taskDtos = tasks.Select(task => new TaskDto
        {
            Id = task.Id,
            Title = task.Title,
            IsDone = task.IsDone
        }).ToList();
        return Ok(taskDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTask(int id)
    {
        var task = await _context.Tasks.FindAsync(id);

        if (task == null)
        {
            return NotFound();
        }

        var taskDto = MapTaskToDto(task);
        return Ok(taskDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask(CreateTaskDto task)
    {
        var newTask = new TaskItem 
        {
            Title = task.Title,
            IsDone = task.IsDone
        };

        _context.Tasks.Add(newTask);
        await _context.SaveChangesAsync();
        
        var taskDto = MapTaskToDto(newTask);

        return CreatedAtAction(
            nameof(GetTask),
            new { id = newTask.Id },
            taskDto);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, UpdateTaskDto updatedTask)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return NotFound();

        task.Title = updatedTask.Title;
        task.IsDone = updatedTask.IsDone;

        await _context.SaveChangesAsync();

        var taskDto = MapTaskToDto(task);
        return Ok(taskDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return NotFound();

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private TaskDto MapTaskToDto(TaskItem task)
    {
        return new TaskDto
        {
            Id = task.Id,
            Title = task.Title,
            IsDone = task.IsDone
        };
    }
}