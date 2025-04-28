using BlueberryHomeworkApp.Application.Usecases.User.FindAllUser;
using MediatR;

namespace BlueberryHomeworkApp.Application.Usecases.User.GetUserByName;

public record GetUserByNameQuery(string Name) : IRequest<IResult<GetUserByNameResult>>
{
}