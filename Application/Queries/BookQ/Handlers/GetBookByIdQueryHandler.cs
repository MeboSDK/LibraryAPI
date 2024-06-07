using Application.Queries.BookQ.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Queries.BookQ.Handlers;

public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book>
{
    private readonly IRepository<Book> _repository;
    public GetBookByIdQueryHandler(IRepository<Book> repository)
    {
        _repository = repository;
    }
    public Task<Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        return _repository.GetByIdAsync(request.Id);
    }
}
