using MediatR;

namespace BlueberryHomeworkApp.Application.Usecases.Name.FindAllName;

public record FindAllNameQuery : IRequest<IResult<List<FindAllNameResult>>>
{
}