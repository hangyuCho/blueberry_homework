using BlueberryHomeworkApp.Infrastructure;
using MediatR;

namespace BlueberryHomeworkApp.Application.Usecases.User.UpdateUser;

public class UpdateUserHandler(
    IUnitOfWork unitOfWork
) : IRequestHandler<UpdateUserCommand, IResult<UpdateUserResult>>
{
    public async Task<IResult<UpdateUserResult>> Handle(UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        // 레포지토리 객체를 취득
        var userRepository = unitOfWork.GetRepository<Domain.Entities.User>();

        // 갱신 대상 취득
        var getResult = await userRepository.GetAsync(request.Id);

        if (getResult.IsError)
        {
            return Result<UpdateUserResult>.Error(getResult.GetError()!);
        }

        var user = getResult.Get();
        user.Name = request.Name;
        user.Email = request.Email;
        user.Role = request.Role;
        user.UpdatedAt = DateTime.UtcNow;

        // 등록 처리 실시
        var addResult = await userRepository.UpdateAsync(user);

        // 처리 중 에러가 발생될 경우
        return addResult.IsError
            ? Result<UpdateUserResult>.Error(addResult.GetError()!)
            : Result<UpdateUserResult>.Ok(new UpdateUserResult(user.Id, user.Name));
    }
}