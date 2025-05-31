namespace BlueberryHomeworkApp.Application.Usecases.User.FindAllUser;

public record FindAllUserResult(
    string Id,
    string Name,
    string? CompanyName,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt);