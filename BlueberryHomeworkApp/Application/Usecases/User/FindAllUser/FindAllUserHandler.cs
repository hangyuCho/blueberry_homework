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

        // 전 유저 정보 취득
        var getResult = await userRepository.FindAllAsync();
        if (getResult.IsError)
        {
            return Result<List<FindAllUserResult>>.Error(getResult.GetError()!);
        }

        var getUserList = getResult.Get();

        // 취득한 정보를 바탕으로 유저아이디로 변환
        var userIds =
            getUserList
                .Select(user => user.Id)
                .Where(userId => string.IsNullOrEmpty(userId) is false)
                .Distinct()
                .ToList();

        var companyRepository = unitOfWork.GetRepository<Domain.Entities.Company>();

        // 유저 아이디 정보와 연동되는 회사 정보를 취득
        var companySpec = new CompanyByIdsSpecification(userIds);
        var companiesResult = await companyRepository.FindAsync(companySpec);
        if (companiesResult.IsError)
        {
            return Result<List<FindAllUserResult>>.Error(companiesResult.GetError()!);
        }

        var companyList = companiesResult.Get().ToList();

        return Result<List<FindAllUserResult>>.Ok(
            getUserList.Select(user =>
                new FindAllUserResult(
                    user.Id,
                    user.Name,
                    CompanyName: user.CompanyId is not null
                        ? companyList.FirstOrDefault(company => company.Id == user.CompanyId)?.Name
                        : null,
                    user.CreatedAt,
                    user.UpdatedAt)).ToList());
    }
}