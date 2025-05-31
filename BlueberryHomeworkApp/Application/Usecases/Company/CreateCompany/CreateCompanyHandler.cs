using BlueberryHomeworkApp.Domain.Exceptions;
using BlueberryHomeworkApp.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace BlueberryHomeworkApp.Application.Usecases.Company.CreateCompany;

[Obsolete("이 클래스는 더 이상 사용되지 않습니다. SignUpCommand를 사용하세요")]
public class CreateCompanyHandler : IRequestHandler<CreateCompanyCommand, IResult<CreateCompanyResult>>
{
    public Task<IResult<CreateCompanyResult>> Handle(CreateCompanyCommand request,
        CancellationToken cancellationToken)
    {
        return Task.FromResult<IResult<CreateCompanyResult>>(Result<CreateCompanyResult>.Ok(null!));
        /*
        // 레포지토리 객체를 취득
        var userRepository = unitOfWork.GetRepository<Domain.Entities.User>();
        var companyRepository = unitOfWork.GetRepository<Domain.Entities.Company>();

        // UserId가 실제로 존재하는지 확인
        var userResult = await userRepository.GetAsync(request.UserId);
        if (userResult is null)
        {
            return Result<CreateCompanyResult>.Error(new Exception("Referenced user does not exist"));
        }

        // 등록 처리 실시
        var companyId = Guid.NewGuid().ToString();
        var addResult = await companyRepository.AddAsync(new Domain.Entities.Company
        {
            Id = companyId,
            Name = request.Name,
            CreatedAt = DateTimeOffset.UtcNow,
            UserId = request.UserId,
        });

        // 유저정보도 갱신
        var user = userResult.Get();
        user.CompanyId = companyId;
        await userRepository.UpdateAsync(user);

        // 처리 중 에러가 발생될 경우
        if (addResult.IsError)
        {
            return Result<CreateCompanyResult>.Error(addResult.GetError()!);
        }

        return Result<CreateCompanyResult>.Ok(
            new CreateCompanyResult(companyId));
            */
    }
}