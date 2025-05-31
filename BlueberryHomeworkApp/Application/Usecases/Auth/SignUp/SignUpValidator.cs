using BlueberryHomeworkApp.Application.Usecases.User.CreateUserById;
using FluentValidation;

namespace BlueberryHomeworkApp.Application.Usecases.Auth.SignUp;

public class SignUpValidator : AbstractValidator<SignUpCommand>
{
    public SignUpValidator()
    {
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters");
    }
}