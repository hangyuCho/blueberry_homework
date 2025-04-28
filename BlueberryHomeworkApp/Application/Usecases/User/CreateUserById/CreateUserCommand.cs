using MediatR;

namespace BlueberryHomeworkApp.Application.Usecases.User.CreateUser;

public record CreateUserCommand(string Name) : IRequest<IResult<CreateUserResult>>
{
}