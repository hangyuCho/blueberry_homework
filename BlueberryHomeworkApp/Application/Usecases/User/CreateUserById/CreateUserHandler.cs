using BlueberryHomeworkApp.Domain.Exceptions;
using BlueberryHomeworkApp.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace BlueberryHomeworkApp.Application.Usecases.User.CreateUser;

public class CreateUserHandler(
    IUnitOfWork unitOfWork
) : IRequestHandler<CreateUserCommand, IResult<CreateUserResult>>
{
    public async Task<IResult<CreateUserResult>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // 레포지토리 객체를 취득
        var userRepository = unitOfWork.GetRepository<Domain.Entities.User>();

        // 등록 처리 실시
        var userId = Guid.NewGuid().ToString();
        var addResult = await userRepository.AddAsync(new Domain.Entities.User
        {
            Id = userId,
            Name = request.Name,
            CreatedAt = DateTimeOffset.UtcNow,
            Company = null
        });

        // 처리 중 에러가 발생될 경우
        if (addResult.IsError)
        {
            return Result<CreateUserResult>.Error(addResult.GetError()!);
        }

        // DB에 데이터를 데이터베이스에 반영
        var saveResult = await unitOfWork.SaveEntitiesAsync(cancellationToken);
        if (saveResult.IsError)
        {
            var isPostgresUniqueError = saveResult.GetError() is DbUpdateException
            {
                InnerException: PostgresException { SqlState: "23505" }
            };
            // 이름이 중복일 경우 에러를 발생시킴
            return isPostgresUniqueError
                ? Result<CreateUserResult>.Error(new ConflictException("name", request.Name))
                : Result<CreateUserResult>.Error(saveResult.GetError()!);
        }
        else
        {
            return Result<CreateUserResult>.Ok(
                new CreateUserResult(userId));
        }
    }
}