using Application.Commands.AuthorC.Commands;
using Domain.Abstractions;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Commands.BookC.Handlers;

public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand>
{
    private readonly IRepository<Author> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<Author> _authorValidator;

    public UpdateAuthorCommandHandler(IRepository<Author> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _repository.GetByIdAsync(request.Id);

        author.Name = request.FirstName;
        author.LastName = request.LastName;
        author.DateOfBirth = request.DateOfBirth;
        author.ModifyDate = DateTime.Now;

        var validationResult = await _authorValidator.ValidateAsync(author);

        if (!validationResult.IsValid)
            throw new InvalidDataException(string.Join(", ", validationResult.Errors));

        _repository.Update(author);

        await _unitOfWork.CommitAsync();
    }
}
