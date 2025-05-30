using BlueberryHomeworkApp.Application.Usecases.User.DeleteUserByName;
using BlueberryHomeworkApp.Infrastructure;
using MediatR;

namespace BlueberryHomeworkApp.Application.Usecases.User.DeleteUserById;

public class DeleteUserByIdHandler(
    IUnitOfWork unitOfWork
) : IRequestHandler<DeleteUserByIdCommand, IResult<DeleteUserByIdResult>>
{
    public async Task<IResult<DeleteUserByIdResult>> Handle(DeleteUserByIdCommand request,
        CancellationToken cancellationToken)
    {
        // 레포지토리 객체를 취득
        var userRepository = unitOfWork.GetRepository<Domain.Entities.User>();

        // 삭제 대상 취득
        var getResult = await userRepository.GetAsync(request.Id);

        if (getResult.IsError)
        {
            return Result<DeleteUserByIdResult>.Error(getResult.GetError()!);
        }

        var getUser = getResult.Get();

        // 삭제 처리 실시
        var deleteResult = await userRepository.DeleteAsync(getUser);

        // 처리 중 에러가 발생될 경우
        if (deleteResult.IsError)
        {
            return Result<DeleteUserByIdResult>.Error(deleteResult.GetError()!);
        }

        return Result<DeleteUserByIdResult>.Ok(
            new DeleteUserByIdResult(getUser.Id));
    }
}