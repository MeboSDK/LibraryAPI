using Domain.Entities;
using FluentValidation;

namespace Application.Commands.AuthorC.Validators;

public class AuthorValidator : AbstractValidator<Author>
{
    public AuthorValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Please ensure that to set a value for {PropertyName}")
            .Must(BeValidName).WithMessage("Please ensure that to set a valid value for {PropertyName}")
            .Length(3, 30);


        RuleFor(x => x.LastName)
     .NotEmpty().WithMessage("Please ensure that to set a value for {PropertyName}")
     .Must(BeValidName).WithMessage("Please ensure that to set a valid value for {PropertyName}")
     .Length(3, 30);

        RuleFor(x => x.DateOfBirth)
          .NotEmpty().WithMessage("Please ensure that to set a value for {PropertyName}");
    }
    private bool BeValidName(string name)
    {
        return name.All(Char.IsLetter);
    }

}
