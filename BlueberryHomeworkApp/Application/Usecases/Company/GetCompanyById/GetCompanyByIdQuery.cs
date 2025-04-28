using BlueberryHomeworkApp.Application.Usecases.User.GetUserByName;
using MediatR;

namespace BlueberryHomeworkApp.Application.Usecases.Company.GetCompanyById;

public record GetCompanyByIdQuery(string Id) : IRequest<IResult<GetCompanyByIdResult>>
{
}