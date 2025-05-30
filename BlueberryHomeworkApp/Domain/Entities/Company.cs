using System.ComponentModel.DataAnnotations;

namespace BlueberryHomeworkApp.Domain.Entities;

public class Company
{
    [MaxLength(50)] public required string Id { get; set; }
    [StringLength(50)] public required string Name { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    [StringLength(24)] public string? UserId { get; set; }
    public virtual User? User { get; set; }
}