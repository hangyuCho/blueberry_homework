namespace BlueberryHomeworkApp.Application.Usecases.Company.GetCompanyById;

public record GetCompanyByIdResult(string Id, string Name, DateTimeOffset CreatedAt, Domain.Entities.User User);