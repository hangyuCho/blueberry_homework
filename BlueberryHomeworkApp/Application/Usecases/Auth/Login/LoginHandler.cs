using System.Security.Authentication;
using BlueberryHomeworkApp.Application.Usecases.Auth.SignUp;
using BlueberryHomeworkApp.Domain.Exceptions;
using BlueberryHomeworkApp.Domain.Specification;
using BlueberryHomeworkApp.Infrastructure;
using MediatR;

namespace BlueberryHomeworkApp.Application.Usecases.Auth.Login;

public class LoginHandler(
    IUnitOfWork unitOfWork
) : IRequestHandler<LoginCommand, IResult<LoginResult>>
{
    public async Task<IResult<LoginResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var userRepository = unitOfWork.GetRepository<Domain.Entities.User>();
        ISpecification<Domain.Entities.User> spec = new UserByEmailSpecification(request.UserEmail);
        var getUserResult = await userRepository.GetAsync(spec);

        if (getUserResult.IsError)
        {
            var error = getUserResult.GetError()!;
            if (error is DataNotFoundException)
            {
                return Result<LoginResult>.Error(new AuthenticationException());
            }
            else
            {
                return Result<LoginResult>.Error(getUserResult.GetError()!);
            }
        }

        var user = getUserResult.Get();

        if (BCrypt.Net.BCrypt.Verify(request.Password, user.Password) is false)
        {
            return Result<LoginResult>.Error(new AuthenticationException());
        }

        return Result<LoginResult>.Ok(new LoginResult(
            UserId: user.Id
        ));
    }
}