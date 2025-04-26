using BlueberryHomeworkApp.Domain.Exceptions;
using BlueberryHomeworkApp.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace BlueberryHomeworkApp.Application.Usecases.User.UpdateUserByName;

public class UpdateUserByNameHandler(
    IUnitOfWork unitOfWork
) : IRequestHandler<UpdateUserByNameCommand, IResult<UpdateUserByNameResult>>
{
    public async Task<IResult<UpdateUserByNameResult>> Handle(UpdateUserByNameCommand request,
        CancellationToken cancellationToken)
    {
        // 레포지토리 객체를 취득
        var userRepository = unitOfWork.GetRepository<Domain.Entities.User>();

        // 갱신 대상 취득
        var getResult = await userRepository.GetAsync(new GetUserByNameSpecification(request.Name));

        if (getResult.IsError)
        {
            return Result<UpdateUserByNameResult>.Error(getResult.GetError()!);
        }

        var user = getResult.Get();
        user.Name = request.Name;
        user.UpdatedAt = DateTime.UtcNow;

        // 등록 처리 실시
        var addResult = await userRepository.UpdateAsync(user);

        // 처리 중 에러가 발생될 경우
        if (addResult.IsError)
        {
            return Result<UpdateUserByNameResult>.Error(addResult.GetError()!);
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
                ? Result<UpdateUserByNameResult>.Error(new ConflictException("name", request.Name))
                : Result<UpdateUserByNameResult>.Error(saveResult.GetError()!);
        }
        else
        {
            return Result<UpdateUserByNameResult>.Ok(new UpdateUserByNameResult(user.Name));
        }
    }
}