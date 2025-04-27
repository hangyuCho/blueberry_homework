namespace BlueberryHomeworkApp.Application.Usecases.User.GetUserByName;

public record GetUserByNameResult(string Id, string Name, DateTimeOffset CreatedAt);