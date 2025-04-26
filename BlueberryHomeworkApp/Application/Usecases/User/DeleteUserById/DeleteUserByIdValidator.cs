using FluentValidation;

namespace BlueberryHomeworkApp.Application.Usecases.User.DeleteUserById;

public class DeleteUserByIdValidator : AbstractValidator<DeleteUserByIdCommand>
{
    public DeleteUserByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");
    }
}