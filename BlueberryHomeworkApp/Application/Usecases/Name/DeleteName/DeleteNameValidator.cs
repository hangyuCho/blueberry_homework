using BlueberryHomeworkApp.Application.Usecases.Name.CreateName;
using FluentValidation;

namespace BlueberryHomeworkApp.Application.Usecases.Name.DeleteName;

public class DeleteNameValidator : AbstractValidator<DeleteNameCommand>
{
    public DeleteNameValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required")
            .GreaterThan(0).WithMessage("Id must be greater than 0");
    }
}