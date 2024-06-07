using Application.Commands.AuthorC.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Commands.AuthorC.Handlers;

public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand>
{
    private readonly IRepository<Author> _authorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddAuthorCommandHandler(IRepository<Author> authorRepository, IUnitOfWork unitOfWork)
    {
        _authorRepository = authorRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(AddAuthorCommand request, CancellationToken cancellationToken)
    {
        Author book = new Author()
        {
            Name = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            InsertDate = DateTime.Now,
        };

        _authorRepository.Add(book);

        await _unitOfWork.CommitAsync();
    }
}
