using Application.Commands.BookC.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Commands.BookC.Handlers;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
{
    private readonly IRepository<Book> _bookRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<BookAuthor> _bookAuthorRepository;
    private readonly IRepository<Author> _authorRepository;

    public UpdateBookCommandHandler(IRepository<Book> bookRepository,
        IUnitOfWork unitOfWork,
        IRepository<BookAuthor> bookAuthorRepository,
        IRepository<Author> authorRepository)
    {
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
        _bookAuthorRepository = bookAuthorRepository;
        _authorRepository = authorRepository;
    }
    public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.Id);

        book.Title = request.Title;
        book.Descrption = request.Descrption;
        book.CurrentCount = request.CurrentCount;
        book.TotalCount = request.TotalCount;
        book.ModifyDate = DateTime.Now;
        book.ImagePath = request.ImagePath;
        book.Rate = request.Rate;
        book.ReleaseDate = request.ReleaseDate;

        _bookRepository.Update(book);
        
        var booksAuthors = await _bookAuthorRepository.GetAllAsync(o => o.BookId == book.Id);
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
                BookId = book.Id
            };

            _bookAuthorRepository.Add(bookAuthor);
        }

        await _unitOfWork.CommitAsync();
    }
}
