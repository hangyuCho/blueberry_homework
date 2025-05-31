using BlueberryHomeworkApp.Application.Usecases.Auth.SignUp;
using FluentValidation;

namespace BlueberryHomeworkApp.Application.Usecases.Auth.Login;

public class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters");
    }
}