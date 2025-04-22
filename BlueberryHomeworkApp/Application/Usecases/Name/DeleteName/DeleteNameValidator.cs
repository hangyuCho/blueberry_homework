using BlueberryHomeworkApp.Application.Usecases.Name.CreateName;
using FluentValidation;

namespace BlueberryHomeworkApp.Application.Usecases.Name.DeleteName;

public abstract class DeleteNameValidator : AbstractValidator<DeleteNameCommand>
{
    protected DeleteNameValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required")
            .GreaterThan(0).WithMessage("Id must be greater than 0");
    }
}