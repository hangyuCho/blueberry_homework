using BlueberryHomeworkApp.Domain.Exceptions;
using BlueberryHomeworkApp.Domain.Specification;
using BlueberryHomeworkApp.Infrastructure;
using MediatR;

namespace BlueberryHomeworkApp.Application.Usecases.Auth.SignUp;

public class SignUpHandler(
    IUnitOfWork unitOfWork
) : IRequestHandler<SignUpCommand, IResult<SignUpResult>>
{
    public async Task<IResult<SignUpResult>> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        // 회사 정보가 있는지 확인
        var hasCompanyData = true;
        var companyRepository = unitOfWork.GetRepository<Domain.Entities.Company>();
        ISpecification<Domain.Entities.Company> companyByNameSpec =
            new CompanyByCompanyNameSpecification(companyName: request.CompanyName);
        var getCompanyResult = await companyRepository.GetAsync(companyByNameSpec);
        if (getCompanyResult.IsError)
        {
            var error = getCompanyResult.GetError()!;

            // 예상 외 에러가 발생한 경우 에러를 발생 시키고 처리를 멈춤
            if (error is not DataNotFoundException)
            {
                return Result<SignUpResult>.Error(getCompanyResult.GetError()!);
            }

            hasCompanyData = false;
        }

        Domain.Entities.Company company;
        // 회사 정보가 존재하지 않을 경우
        if (hasCompanyData is false)
        {
            // 회사 정보를 생성
            company = new Domain.Entities.Company
            {
                Id = Guid.NewGuid().ToString(),
                Address = request.CompanyAddress,
                Name = request.CompanyName,
                CreatedAt = DateTimeOffset.UtcNow,
                IsActive = true
            };
            var createCompanyResult = await companyRepository.AddAsync(company);
            if (createCompanyResult.IsError)
            {
                return Result<SignUpResult>.Error(createCompanyResult.GetError()!);
            }
        }
        else
        {
            company = getCompanyResult.Get();
        }

        // User 생성
        var user = new Domain.Entities.User
        {
            Id = Guid.NewGuid()
                .ToString(),
            Email = request.UserEmail,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Name = request.UserName,
            CompanyId = company.Id,
            CreatedAt = DateTimeOffset.UtcNow,
        };

        // 유저 정보 등록
        var userRepository = unitOfWork.GetRepository<Domain.Entities.User>();
        var createUserResult = await userRepository.AddAsync(user);
        if (createUserResult.IsError)
        {
            return Result<SignUpResult>.Error(createUserResult.GetError()!);
        }

        return Result<SignUpResult>.Ok(new SignUpResult(
            UserId: user.Id,
            CompanyId: company.Id
        ));
    }
}