using BlueberryHomeworkApp.Application.Usecases.User.FindAllUser;
using BlueberryHomeworkApp.Domain.Specification;
using BlueberryHomeworkApp.Infrastructure;
using MediatR;

namespace BlueberryHomeworkApp.Application.Usecases.User.GetUserByName;

public class GetUserByNameHandler(
    IUnitOfWork unitOfWork
) : IRequestHandler<GetUserByNameQuery, IResult<GetUserByNameResult>>
{
    public async Task<IResult<GetUserByNameResult>> Handle(GetUserByNameQuery request,
        CancellationToken cancellationToken)
    {
        // 레포지토리 객체를 취득
        var userRepository = unitOfWork.GetRepository<Domain.Entities.User>();

        // 대상 취득
        ISpecification<Domain.Entities.User> spec = new GetUserByNameSpecification(request.Name);
        var getResult = await userRepository.GetAsync(spec);

        if (getResult.IsError)
        {
            return Result<GetUserByNameResult>.Error(getResult.GetError()!);
        }

        var user = getResult.Get();

        return Result<GetUserByNameResult>.Ok(
            new GetUserByNameResult(
                user.Id,
                user.Name,
                user.CreatedAt,
                user.UpdatedAt));
    }
}