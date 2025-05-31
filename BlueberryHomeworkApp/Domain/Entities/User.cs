using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlueberryHomeworkApp.Domain.Entities;

public class User
{
    [MaxLength(50)] public required string Id { get; set; }

    public required string Name { get; set; }

    public string? CompanyId { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public bool IsActive { get; set; }
}