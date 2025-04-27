using BlueberryHomeworkApp.Domain.Specification;
using BlueberryHomeworkApp.Infrastructure;
using MediatR;

namespace BlueberryHomeworkApp.Application.Usecases.User.DeleteUserByName;

public class DeleteUserByNameHandler(
    IUnitOfWork unitOfWork
) : IRequestHandler<DeleteUserByNameCommand, IResult<DeleteUserByNameResult>>
{
    public async Task<IResult<DeleteUserByNameResult>> Handle(DeleteUserByNameCommand request,
        CancellationToken cancellationToken)
    {
        // 레포지토리 객체를 취득
        var userRepository = unitOfWork.GetRepository<Domain.Entities.User>();

        // 삭제 대상 취득
        ISpecification<Domain.Entities.User> spec = new GetUserByNameSpecification(request.Name);
        var getResult = await userRepository.GetAsync(spec);

        if (getResult.IsError)
        {
            return Result<DeleteUserByNameResult>.Error(getResult.GetError()!);
        }

        var getUser = getResult.Get();

        // 삭제 처리 실시
        var deleteResult = await userRepository.DeleteAsync(getUser);

        // 처리 중 에러가 발생될 경우
        if (deleteResult.IsError)
        {
            return Result<DeleteUserByNameResult>.Error(deleteResult.GetError()!);
        }

        // DB에 데이터를 데이터베이스에 반영
        await unitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result<DeleteUserByNameResult>.Ok(
            new DeleteUserByNameResult(getUser.Name));
    }
}