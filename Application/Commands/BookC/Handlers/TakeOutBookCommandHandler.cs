using Application.Commands.BookC.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Commands.BookC.Handlers;

public class TakeOutBookCommandHandler : IRequestHandler<TakeOutBookCommand>
{
    private readonly IRepository<UserBook> _userBookRepository;
    private readonly IRepository<Book> _bookRepository;
    private readonly IUnitOfWork _unitOfWork;
    public TakeOutBookCommandHandler(IRepository<Book> bookRepository,
        IRepository<UserBook> userBookRepository,
        IUnitOfWork unitOfWork)
    {
        _userBookRepository = userBookRepository;
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(TakeOutBookCommand request, CancellationToken cancellationToken)
    {
        UserBook userBook = new UserBook()
        {
            UserId = request.UsedId,
            BookId = request.BookId,
            BooksCount = request.Count
        };

        _userBookRepository.Add(userBook);

        var book = await _bookRepository.GetByIdAsync(request.BookId);
        book.CurrentCount -= request.Count;

        _bookRepository.Update(book);

        await _unitOfWork.CommitAsync();
    }
}
