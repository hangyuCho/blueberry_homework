using BlueberryHomeworkApp.Domain.Entities;
using MediatR;

namespace BlueberryHomeworkApp.Application.Usecases.User.UpdateUser;

public record UpdateUserCommand(
    string Id,
    string Name,
    string Email,
    UserRole Role
) : IRequest<IResult<UpdateUserResult>>
{
}