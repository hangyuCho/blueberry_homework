using MediatR;

namespace BlueberryHomeworkApp.Application.Usecases.User.FindAllUser;

public record FindAllUserQuery : IRequest<IResult<List<FindAllUserResult>>>
{
}