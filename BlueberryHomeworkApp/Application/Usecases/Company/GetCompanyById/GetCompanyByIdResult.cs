namespace BlueberryHomeworkApp.Application.Usecases.Company.GetCompanyById;

public record GetCompanyByIdResult(string Id, string Name, DateTimeOffset CreatedAt, List<Domain.Entities.User> Users);