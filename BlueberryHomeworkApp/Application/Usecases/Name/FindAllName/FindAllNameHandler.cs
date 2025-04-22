using BlueberryHomeworkApp.Domain.Entities;
using BlueberryHomeworkApp.Infrastructure;
using MediatR;

namespace BlueberryHomeworkApp.Application.Usecases.Name.FindAllName;

public class FindAllNameHandler(
    IUnitOfWork unitOfWork
) : IRequestHandler<FindAllNameQuery, IResult<List<FindAllNameResult>>>
{
    public async Task<IResult<List<FindAllNameResult>>> Handle(FindAllNameQuery request,
        CancellationToken cancellationToken)
    {
        // 레포지토리 객체를 취득
        var personNameRepository = unitOfWork.GetRepository<PersonName>();

        // 삭제 대상 취득
        var getResult = await personNameRepository.FindAllAsync();

        if (getResult.IsError)
        {
            return Result<List<FindAllNameResult>>.Error(getResult.GetError()!);
        }

        var getPersonNameList = getResult.Get();

        return Result<List<FindAllNameResult>>.Ok(
            getPersonNameList.Select(personName =>
                new FindAllNameResult(
                    personName.Id,
                    personName.Name,
                    personName.CreatedAt)).ToList());
    }
}