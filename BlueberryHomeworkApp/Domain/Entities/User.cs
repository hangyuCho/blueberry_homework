using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Mongo.Migration.Documents;

namespace BlueberryHomeworkApp.Domain.Entities;

public class User
{
    [MaxLength(50)] public required string Id { get; set; }

    public required string Name { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    [JsonIgnore] public virtual Company? Company { get; set; }

    public DocumentVersion Version { get; set; }

    public bool IsActive { get; set; }
}