using MediatR;

namespace BlueberryHomeworkApp.Application.Usecases.Name.CreateName;

public record CreateNameCommand(string Name) : IRequest<IResult>
{
}