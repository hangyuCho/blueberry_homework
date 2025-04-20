using FluentValidation;

namespace BlueberryHomeworkApp.Domain.CreateName;

public class CreateNameCommandValidator : AbstractValidator<CreateNameCommand>
{
    public CreateNameCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters");
    }
        
}