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

        var userRepository = unitOfWork.GetRepository<Domain.Entities.User>();
        var getUserResult = await userRepository.GetAsync(company.UserId);

        if (getUserResult.IsError)
        {
            return Result<GetCompanyByIdResult>.Error(getUserResult.GetError()!);
        }

        var user = getUserResult.Get();

        return Result<GetCompanyByIdResult>.Ok(
            new GetCompanyByIdResult(
                Id: company.Id,
                Name: company.Name,
                CreatedAt: company.CreatedAt,
                UserId: user.Id,
                UserName: user.Name
            ));
    }
}