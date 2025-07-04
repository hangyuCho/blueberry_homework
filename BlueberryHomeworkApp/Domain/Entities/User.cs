using System.Text.Json.Serialization;

namespace BlueberryHomeworkApp.Domain.Entities;

public class User
{
    public required string Id { get; set; }

    public required string Name { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    [JsonIgnore] public virtual Company? Company { get; set; }
}