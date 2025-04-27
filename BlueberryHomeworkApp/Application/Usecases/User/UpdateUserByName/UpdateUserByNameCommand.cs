using MediatR;

namespace BlueberryHomeworkApp.Application.Usecases.User.UpdateUserByName;

public record UpdateUserByNameCommand(string Id, string Name) : IRequest<IResult<UpdateUserByNameResult>>
{
}