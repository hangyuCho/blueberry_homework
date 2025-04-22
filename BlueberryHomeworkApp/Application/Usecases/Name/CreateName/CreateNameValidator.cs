using FluentValidation;

namespace BlueberryHomeworkApp.Application.Usecases.Name.CreateName;

public class CreateNameValidator : AbstractValidator<CreateNameCommand>
{
    public CreateNameValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters");
    }
}