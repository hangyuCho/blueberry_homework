using MediatR;

namespace BlueberryHomeworkApp.Application.Usecases.Auth.SignUp;

public record SignUpCommand(
    string UserName,
    string UserEmail,
    string Password,
    string CompanyName,
    string CompanyAddress) : IRequest<IResult<SignUpResult>>
{
}