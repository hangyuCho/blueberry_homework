using BlueberryHomeworkApp.Infrastructure;
using MediatR;

namespace BlueberryHomeworkApp.Application.Usecases.User.FindAllUser;

public class FindAllUserHandler(
    IUnitOfWork unitOfWork
) : IRequestHandler<FindAllUserQuery, IResult<List<FindAllUserResult>>>
{
    public async Task<IResult<List<FindAllUserResult>>> Handle(FindAllUserQuery request,
        CancellationToken cancellationToken)
    {
        // 레포지토리 객체를 취득
        var userRepository = unitOfWork.GetRepository<Domain.Entities.User>();

        // 삭제 대상 취득
        var getResult = await userRepository.FindAllAsync();

        if (getResult.IsError)
        {
            return Result<List<FindAllUserResult>>.Error(getResult.GetError()!);
        }

        var getUserList = getResult.Get();

        return Result<List<FindAllUserResult>>.Ok(
            getUserList.Select(user =>
                new FindAllUserResult(
                    user.Id,
                    user.Name,
                    user.CreatedAt)).ToList());
    }
}