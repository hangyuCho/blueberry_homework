using BlueberryHomeworkApp.Domain.Entities;
using BlueberryHomeworkApp.Infrastructure;
using MediatR;

namespace BlueberryHomeworkApp.Application.Usecases.Name.DeleteName;

public class DeleteNameHandler(
    IUnitOfWork unitOfWork
) : IRequestHandler<DeleteNameCommand, IResult>
{
    public async Task<IResult> Handle(DeleteNameCommand request, CancellationToken cancellationToken)
    {
        // 레포지토리 객체를 취득
        var personNameRepository = unitOfWork.GetRepository<PersonName>();

        // 삭제 대상 취득
        var getResult = await personNameRepository.GetAsync(request.Id);

        if (getResult.IsError)
        {
            return getResult.AsError();
        }

        var getPersonName = getResult.Get();

        // 삭제 처리 실시
        var deleteResult = await personNameRepository.DeleteAsync(getPersonName);

        // 처리 중 에러가 발생될 경우
        if (deleteResult.IsError)
        {
            return deleteResult.AsError();
        }

        // DB에 데이터를 데이터베이스에 반영
        await unitOfWork.CommitAsync(cancellationToken);

        return Result.Ok();
    }
}