using BlueberryHomeworkApp.Infrastructure;
using MediatR;

namespace BlueberryHomeworkApp.Application.Usecases.Company.GetCompanyById;

public class GetCompanyByIdHandler(
    IUnitOfWork unitOfWork
) : IRequestHandler<GetCompanyByIdQuery, IResult<GetCompanyByIdResult>>
{
    public async Task<IResult<GetCompanyByIdResult>> Handle(GetCompanyByIdQuery request,
        CancellationToken cancellationToken)
    {
        // 레포지토리 객체를 취득
        var companyRepository = unitOfWork.GetRepository<Domain.Entities.Company>();

        // 대상 취득
        var getResult = await companyRepository.GetAsync(request.Id);

        if (getResult.IsError)
        {
            return Result<GetCompanyByIdResult>.Error(getResult.GetError()!);
        }

        var company = getResult.Get();

        // 회사에 소속된 모든 직원을 취득
        var userRepository = unitOfWork.GetRepository<Domain.Entities.User>();
        var findUserSpec = new UserByCompanyIdSpecification(company.Id);
        var findUserResult = await userRepository.FindAsync(findUserSpec);

        if (findUserResult.IsError)
        {
            return Result<GetCompanyByIdResult>.Error(findUserResult.GetError()!);
        }

        return Result<GetCompanyByIdResult>.Ok(
            new GetCompanyByIdResult(
                Id: company.Id,
                Name: company.Name,
                CreatedAt: company.CreatedAt,
                Users: findUserResult.Get()
            ));
    }
}