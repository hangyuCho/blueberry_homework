using BlueberryHomeworkApp.Domain.Entities;
using BlueberryHomeworkApp.Infrastructure;
using MediatR;

namespace BlueberryHomeworkApp.Application.Usecases.Name.CreateName;

public class CreateNameHandler(
    IUnitOfWork unitOfWork
) : IRequestHandler<CreateNameCommand, IResult>
{
    public async Task<IResult> Handle(CreateNameCommand request, CancellationToken cancellationToken)
    {
        // 레포지토리 객체를 취득
        var personNameRepository = unitOfWork.GetRepository<PersonName>();

        // 등록 처리 실시
        var addResult = await personNameRepository.AddAsync(new PersonName
        {
            Name = request.Name,
            CreatedAt = DateTimeOffset.UtcNow
        });

        // 처리 중 에러가 발생될 경우
        if (addResult.IsError)
        {
            return addResult.AsError();
        }

        // DB에 데이터를 데이터베이스에 반영
        var saveResult = await unitOfWork.CommitAsync(cancellationToken);
        if (saveResult.IsError)
        {
            return addResult.AsError();
        }

        return Result.Ok();
    }
}