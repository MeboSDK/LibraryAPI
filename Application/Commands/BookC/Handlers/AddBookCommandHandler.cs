using Application.Commands.BookC.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Commands.BookC.Handlers;

public class AddBookCommandHandler : IRequestHandler<AddBookCommand>
{
    private readonly IRepository<Book> _bookRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddBookCommandHandler(IRepository<Book> bookRepository, IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
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
            ImagePath = request.ImagePath,
            Rate = request.Rate,
            ReleaseDate = request.ReleaseDate,
        };

        await _bookRepository.AddAsync(book);

        await _unitOfWork.CommitAsync();
    }
}
