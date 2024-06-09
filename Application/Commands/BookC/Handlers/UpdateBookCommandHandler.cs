using Application.Commands.BookC.Commands;
using Application.Commands.BookC.Validators;
using Domain.Abstractions;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Commands.BookC.Handlers;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
{
    private readonly IRepository<Book> _bookRepository;
    private readonly IRepository<BookAuthor> _bookAuthorRepository;
    private readonly IRepository<Author> _authorRepository;
    private readonly IValidator<Book> _bookValidator;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBookCommandHandler(IRepository<Book> bookRepository,
        IUnitOfWork unitOfWork,
        IRepository<BookAuthor> bookAuthorRepository,
        IRepository<Author> authorRepository, IValidator<Book> bookValidator)
    {
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
        _bookAuthorRepository = bookAuthorRepository;
        _authorRepository = authorRepository;
        _bookValidator = bookValidator;
    }
    public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.Id);

        book.Title = request.Title;
        book.Descrption = request.Descrption;
        book.CurrentCount = request.CurrentCount;
        book.TotalCount = request.TotalCount;
        book.ModifyDate = DateTime.Now;
        book.Rate = request.Rate;
        book.ReleaseDate = request.ReleaseDate;

        var validationResult = await _bookValidator.ValidateAsync(book);

        if (!validationResult.IsValid)
            throw new InvalidDataException(string.Join(", ", validationResult.Errors));

        _bookRepository.Update(book);

        await UpdateAuthorsInBookAsync(request);

        await _unitOfWork.CommitAsync();
    }

    private async Task UpdateAuthorsInBookAsync(UpdateBookCommand request)
    {
        var booksAuthors = await _bookAuthorRepository.GetAllAsync(o => o.BookId == request.Id);
        var deleteableAuthorsIds = booksAuthors.Where(o => !request.AuthorsIds.Contains(o.AuthorId));

        for (int i = 0; i < deleteableAuthorsIds.Count(); i++)
            _bookAuthorRepository.Remove(deleteableAuthorsIds.ElementAt(i));

        var existsAuthorsIds = booksAuthors.Where(o => request.AuthorsIds.Contains(o.AuthorId)).Select(o => o.AuthorId);

        var addableAuthors = await _authorRepository.GetAllAsync(o => request.AuthorsIds.Contains(o.Id) && !existsAuthorsIds.Contains(o.Id));

        for (int i = 0; i < addableAuthors.Count(); i++)
        {
            BookAuthor bookAuthor = new()
            {
                AuthorId = addableAuthors.ElementAt(i).Id,
                BookId = request.Id
            };

            _bookAuthorRepository.Add(bookAuthor);
        }

    }

}
