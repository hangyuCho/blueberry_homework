namespace BlueberryHomeworkApp.Domain.Entities;

public class PersonName
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
}