using System.ComponentModel.DataAnnotations;

namespace BlueberryHomeworkApp.Domain.Entities;

public class Company
{
    [MaxLength(50)] public required string Id { get; set; }
    [StringLength(50)] public required string Name { get; set; }

    [MaxLength(50)] public required string Address { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}