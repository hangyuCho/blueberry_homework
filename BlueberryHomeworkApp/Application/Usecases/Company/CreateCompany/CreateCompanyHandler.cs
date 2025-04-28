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

        // DB에 데이터를 데이터베이스에 반영
        var saveResult = await unitOfWork.SaveEntitiesAsync(cancellationToken);
        if (saveResult.IsError)
        {
            var isPostgresUniqueError = saveResult.GetError() is DbUpdateException
            {
                InnerException: PostgresException { SqlState: "23505" }
            };
            // 이름이 중복일 경우 에러를 발생시킴
            return isPostgresUniqueError
                ? Result<CreateCompanyResult>.Error(new ConflictException("name", request.Name))
                : Result<CreateCompanyResult>.Error(saveResult.GetError()!);
        }
        else
        {
            return Result<CreateCompanyResult>.Ok(
                new CreateCompanyResult(companyId));
        }
    }
}