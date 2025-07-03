using System;
using System.ComponentModel.DataAnnotations;

namespace NotesAPI.Models;

public class Note
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]

    public string Title { get; set; } = string.Empty;

    public string? Content { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
