using MediatR;

namespace BlueberryHomeworkApp.Application.Usecases.Name.DeleteName;

public record DeleteNameCommand(int Id) : IRequest<IResult>
{
}