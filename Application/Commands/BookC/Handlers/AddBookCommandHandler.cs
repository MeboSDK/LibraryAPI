using Application.Commands.AuthorC.Validators;
using Application.Commands.BookC.Commands;
using Domain.Abstractions;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Commands.BookC.Handlers;

public class AddBookCommandHandler : IRequestHandler<AddBookCommand>
{
    private readonly IRepository<Book> _bookRepository;
    private readonly IRepository<BookAuthor> _bookAuthorRepository;
    private readonly IRepository<Author> _authorRepository;
    private readonly IValidator<Book> _bookValidator;

    private readonly IUnitOfWork _unitOfWork;

    public AddBookCommandHandler(IRepository<Book> bookRepository,
        IRepository<BookAuthor> bookAuthorRepository,
        IRepository<Author> authorRepository,
        IUnitOfWork unitOfWork,
        IRepository<BookImage> imageRepository,
        IValidator<Book> bookValidator)
    {
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
        _bookAuthorRepository = bookAuthorRepository;
        _authorRepository = authorRepository;
        _bookValidator = bookValidator;
    }
    public async Task Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        Book book = new Book()
        {
            Title = request.Title,
            Descrption = request.Descrption,
            CurrentCount = request.CurrentCount,
            TotalCount = request.TotalCount,
            InsertDate = DateTime.Now,
            Rate = request.Rate,
            ReleaseDate = request.ReleaseDate,
        };

        var validationResult = await _bookValidator.ValidateAsync(book);

        if (!validationResult.IsValid)
            throw new InvalidDataException(string.Join(", ", validationResult.Errors));

        _bookRepository.Add(book);

        await _unitOfWork.CommitAsync();

        await AddAuthorsInBookAsync(request,book.Id);

        await _unitOfWork.CommitAsync();
    }

    private async Task AddAuthorsInBookAsync(AddBookCommand request, int bookId)
    {
        var authors = await _authorRepository.GetAllAsync(e => request.AuthorsIds.Contains(e.Id));

        for (int i = 0; i < authors.Count(); i++)
        {
            BookAuthor bookAuthor = new()
            {
                AuthorId = authors.ElementAt(i).Id,
                BookId = bookId
            };

            _bookAuthorRepository.Add(bookAuthor);
        }
    }
}
