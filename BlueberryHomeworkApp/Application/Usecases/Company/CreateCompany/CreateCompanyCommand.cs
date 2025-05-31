using MediatR;

namespace BlueberryHomeworkApp.Application.Usecases.Company.CreateCompany;

public record CreateCompanyCommand(string Name, string UserId) : IRequest<IResult<CreateCompanyResult>>
{
}