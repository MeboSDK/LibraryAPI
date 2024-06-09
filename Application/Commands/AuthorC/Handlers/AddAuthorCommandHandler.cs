using Application.Commands.AuthorC.Commands;
using Domain.Abstractions;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Commands.AuthorC.Handlers;

public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand>
{
    private readonly IRepository<Author> _authorRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<Author> _authorValidator;

    public AddAuthorCommandHandler(IRepository<Author> authorRepository, IUnitOfWork unitOfWork)
    {
        _authorRepository = authorRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(AddAuthorCommand request, CancellationToken cancellationToken)
    {
        Author author = new Author()
        {
            Name = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            InsertDate = DateTime.Now,
        };

        var validationResult = await _authorValidator.ValidateAsync(author);

        if (!validationResult.IsValid)
            throw new InvalidDataException(string.Join(", ", validationResult.Errors));

        _authorRepository.Add(author);

        await _unitOfWork.CommitAsync();
    }
}
