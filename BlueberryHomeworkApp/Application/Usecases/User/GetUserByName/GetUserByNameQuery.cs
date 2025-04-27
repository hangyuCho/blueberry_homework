using BlueberryHomeworkApp.Application.Usecases.User.FindAllUser;
using MediatR;

namespace BlueberryHomeworkApp.Application.Usecases.User.GetUserByName;

public record GetUserByNameQuery : IRequest<IResult<List<FindAllUserResult>>>
{
}