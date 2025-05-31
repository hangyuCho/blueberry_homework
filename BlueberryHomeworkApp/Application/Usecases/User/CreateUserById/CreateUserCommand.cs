using MediatR;

namespace BlueberryHomeworkApp.Application.Usecases.User.CreateUserById;

public record CreateUserCommand(string Name) : IRequest<IResult<CreateUserResult>>
{
}