using System.ComponentModel.DataAnnotations;

namespace TaskApi.Dtos;

public class CreateTaskDto
{
    [Required]
    [StringLength(100)]
    public string Title { get; set; } = "";
    public bool IsDone { get; set; }
}