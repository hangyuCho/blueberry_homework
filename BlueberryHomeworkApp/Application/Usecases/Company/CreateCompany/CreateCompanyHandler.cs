using BlueberryHomeworkApp.Domain.Exceptions;
using BlueberryHomeworkApp.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace BlueberryHomeworkApp.Application.Usecases.Company.CreateCompany;

public class CreateCompanyHandler(
    IUnitOfWork unitOfWork
) : IRequestHandler<CreateCompanyCommand, IResult<CreateCompanyResult>>
{
    public async Task<IResult<CreateCompanyResult>> Handle(CreateCompanyCommand request,
        CancellationToken cancellationToken)
    {
        // 레포지토리 객체를 취득
        var companyRepository = unitOfWork.GetRepository<Domain.Entities.Company>();

        // 등록 처리 실시
        var companyId = Guid.NewGuid().ToString();
        var addResult = await companyRepository.AddAsync(new Domain.Entities.Company
        {
            Id = companyId,
            Name = request.Name,
            CreatedAt = DateTimeOffset.UtcNow,
            UserId = request.UserId,
        });

        // 처리 중 에러가 발생될 경우
        if (addResult.IsError)
        {
            return Result<CreateCompanyResult>.Error(addResult.GetError()!);
        }

        return Result<CreateCompanyResult>.Ok(
            new CreateCompanyResult(companyId));
    }
}