using MediatR;

namespace BlueberryHomeworkApp.Application.Usecases.Auth.Login;

public record LoginCommand(
    string UserEmail,
    string Password
) : IRequest<IResult<LoginResult>>
{
}