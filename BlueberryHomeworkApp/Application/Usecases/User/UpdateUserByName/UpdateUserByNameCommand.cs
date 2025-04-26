using MediatR;

namespace BlueberryHomeworkApp.Application.Usecases.User.UpdateUserByName;

public record UpdateUserByNameCommand(string Name) : IRequest<IResult>, IRequest<IResult<UpdateUserByNameResult>>
{
}