using Application.Commands.ImageC.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Commands.ImageC.Handlers;

public class AddBookImageCommandHandler : IRequestHandler<AddBookImageCommand>
{
    public readonly IRepository<BookImage> _bookImageRepository;
    public readonly IUnitOfWork _unitOfWork;
    public AddBookImageCommandHandler(IRepository<BookImage> bookImageRepository, IUnitOfWork unitOfWork)
    {
        _bookImageRepository = bookImageRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(AddBookImageCommand request, CancellationToken cancellationToken)
    {
        BookImage bookImage = new BookImage()
        {
            FileName = request.FileName,
            ContentType = request.ContentType,
            Image = request.Image,
            BookId = request.BookId
        };

        _bookImageRepository.Add(bookImage);
         await _unitOfWork.CommitAsync();
    }
}
