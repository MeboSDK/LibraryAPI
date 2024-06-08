using Application.Commands.BookC.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Commands.BookC.Handlers;

public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand>
{
    private readonly IRepository<Book> _bookRepository;
    private readonly IRepository<UserBook> _userBookRepository;
    private readonly IUnitOfWork _unitOfWork;
    public ReturnBookCommandHandler(IRepository<Book> bookRepository, IRepository<UserBook> userBookRepository, IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _userBookRepository = userBookRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ReturnBookCommand request, CancellationToken cancellationToken)
    {
        var record = await _userBookRepository.GetByIdAsync(request.Id);

        var book = await _bookRepository.GetByIdAsync(record.BookId);

        book.CurrentCount += request.Count;

        _bookRepository.Update(book);

        record.BooksCount -= request.Count;
        
        if (record.BooksCount <= 0)
            _userBookRepository.Remove(record);
        else
            _userBookRepository.Update(record);
        
        await _unitOfWork.CommitAsync();
    }
}
