using MediatR;

namespace BlueberryHomeworkApp.Application.Usecases.User.DeleteUserById;

public record DeleteUserByIdCommand(string Id) : IRequest<IResult<DeleteUserByIdResult>>
{
}