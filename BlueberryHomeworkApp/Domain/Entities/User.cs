using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlueberryHomeworkApp.Domain.Entities;

public class User
{
    [MaxLength(50)] public required string Id { get; set; }

    public required string Name { get; set; }


    [MaxLength(320)] public required string? Email { get; set; }

    [MaxLength(50)] public required string Password { get; set; }

    public UserRole Role { get; set; } = UserRole.Worker;

    public string? CompanyId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public bool IsActive { get; set; }
}