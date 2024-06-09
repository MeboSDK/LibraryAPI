using Domain.Entities;
using FluentValidation;

namespace Application.Commands.BookC.Validators;

public class BookValidator : AbstractValidator<Book>
{
    public BookValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Please ensure that to set a value for {PropertyName}");

        RuleFor(x => x.Rate)
            .NotEmpty().WithMessage("Please ensure that to set a value for {PropertyName}");

        RuleFor(x => x.ReleaseDate)
            .NotEmpty().WithMessage("Please ensure that to set a value for {PropertyName}");
    }
}
