using MediatR;

namespace BlueberryHomeworkApp.Application.Usecases.User.DeleteUserByName;

public record DeleteUserByNameCommand(string Name) : IRequest<IResult<DeleteUserByNameResult>>
{
}