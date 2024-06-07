using Application.Commands.BookC.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Commands.BookC.Handlers;

public class AddBookCommandHandler : IRequestHandler<AddBookCommand>
{
    private readonly IRepository<Book> _bookRepository;
    private readonly IRepository<BookAuthor> _bookAuthorRepository;
    private readonly IRepository<Author> _authorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddBookCommandHandler(IRepository<Book> bookRepository, 
        IRepository<BookAuthor> bookAuthorRepository,
        IRepository<Author> authorRepository,
        IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
        _bookAuthorRepository = bookAuthorRepository;
        _authorRepository = authorRepository;
    }
    public async Task Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        var authors = await _authorRepository.GetAllAsync(e => request.AuthorsIds.Contains(e.Id));

        Book book = new Book()
        {
            Title = request.Title,
            Descrption = request.Descrption,
            CurrentCount = request.CurrentCount,
            TotalCount = request.TotalCount,
            InsertDate = DateTime.Now,
            ImagePath = request.ImagePath,
            Rate = request.Rate,
            ReleaseDate = request.ReleaseDate,
        };

        _bookRepository.Add(book);

        await _unitOfWork.CommitAsync();

        for (int i = 0; i < authors.Count(); i++)
        {
            BookAuthor bookAuthor = new()
            {
                AuthorId = authors.ElementAt(i).Id,
                BookId = book.Id
            };

            _bookAuthorRepository.Add(bookAuthor);
        }

        await _unitOfWork.CommitAsync();

    }
}
