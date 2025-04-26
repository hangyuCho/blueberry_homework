using BlueberryHomeworkApp.Application.Usecases.User.CreateUser;
using FluentValidation;

namespace BlueberryHomeworkApp.Application.Usecases.User.UpdateUserByName;

public class UpdateUserByNameValidator : AbstractValidator<CreateUserCommand>
{
    public UpdateUserByNameValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters");
    }
}