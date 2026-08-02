
using System.ComponentModel.DataAnnotations;

namespace TaskApi.Models;

public class TaskItem
{
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public string Title { get; set; } = "";
    public bool IsDone { get; set; }
}