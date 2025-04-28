using BlueberryHomeworkApp.Application.Usecases.User.GetUserByName;
using BlueberryHomeworkApp.Domain.Specification;
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

        return Result<GetCompanyByIdResult>.Ok(
            new GetCompanyByIdResult(
                company.Id,
                company.Name,
                company.CreatedAt,
                company.User
            ));
    }
}