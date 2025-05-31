using FluentValidation;

namespace BlueberryHomeworkApp.Application.Usecases.Company.CreateCompany;

public class CreateCompanyValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required")
            .MaximumLength(36).WithMessage("UserId must not exceed 36 characters");
    }
}