using BlueberryHomeworkApp.Infrastructure;
using MediatR;

namespace BlueberryHomeworkApp.Application.Usecases.User.CreateUserById;

[Obsolete("이 클래스는 더 이상 사용되지 않습니다. SignUpCommand를 사용하세요")]
public class CreateUserHandler : IRequestHandler<CreateUserCommand, IResult<CreateUserResult>>
{
    public Task<IResult<CreateUserResult>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult<IResult<CreateUserResult>>(Result<CreateUserResult>.Ok(null!));
        /*
        // 레포지토리 객체를 취득
        var userRepository = unitOfWork.GetRepository<Domain.Entities.User>();

        // 등록 처리 실시
        var userId = Guid.NewGuid().ToString();
        var addResult = await userRepository.AddAsync(new Domain.Entities.User
        {
            Id = userId,
            Name = request.Name,
            CreatedAt = DateTimeOffset.UtcNow
        });

        // 처리 중 에러가 발생될 경우
        if (addResult.IsError)
        {
            return Result<CreateUserResult>.Error(addResult.GetError()!);
        }

        // DB에 데이터를 데이터베이스에 반영
        return Result<CreateUserResult>.Ok(
            new CreateUserResult(userId));
            */
    }
}