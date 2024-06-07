using Application.Commands.BookC.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Commands.BookC.Handlers;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
{
    private readonly IRepository<Book> _bookRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBookCommandHandler(IRepository<Book> bookRepository, IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
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

        await _bookRepository.UpdateAsync(book);

        await _unitOfWork.CommitAsync();
    }
}
