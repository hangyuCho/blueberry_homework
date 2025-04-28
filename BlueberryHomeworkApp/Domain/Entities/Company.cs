namespace BlueberryHomeworkApp.Domain.Entities;

public class Company
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public string UserId { get; set; }
    public virtual User User { get; set; }
}