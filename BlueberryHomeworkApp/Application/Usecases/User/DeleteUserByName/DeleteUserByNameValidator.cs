using BlueberryHomeworkApp.Application.Usecases.User.DeleteUserById;
using FluentValidation;

namespace BlueberryHomeworkApp.Application.Usecases.User.DeleteUserByName;

public class DeleteUserByNameValidator : AbstractValidator<DeleteUserByNameCommand>
{
    public DeleteUserByNameValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters");
    }
}