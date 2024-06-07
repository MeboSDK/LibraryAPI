using Application.Queries.BookQ.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Queries.BookQ.Handlers;

public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, IEnumerable<Book>>
{
    private readonly IRepository<Book> _repository;
    public GetAllBooksQueryHandler(IRepository<Book> repository)
    {
        _repository = repository;
    }
    public Task<IEnumerable<Book>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        return _repository.GetAllAsync(request.filter);
    }
}