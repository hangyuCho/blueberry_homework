using BlueberryHomeworkApp.Application.Usecases.User.CreateUser;
using FluentValidation;

namespace BlueberryHomeworkApp.Application.Usecases.User.GetUserByName;

public class GetUserByNameValidator : AbstractValidator<GetUserByNameQuery>
{
    public GetUserByNameValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters");
    }
}