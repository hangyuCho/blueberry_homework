using BlueberryHomeworkApp.Application.Usecases.User.CreateUser;
using FluentValidation;

namespace BlueberryHomeworkApp.Application.Usecases.User.UpdateUserByName;

public class UpdateUserByNameValidator : AbstractValidator<UpdateUserByNameCommand>
{
    public UpdateUserByNameValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters");
    }
}