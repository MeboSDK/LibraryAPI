using Application.Queries.AuthorQ.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Queries.AuthorQ.Handlers;

public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, Author>
{
    private readonly IRepository<Author> _repository;
    public GetAuthorByIdQueryHandler(IRepository<Author> repository)
    {
        _repository = repository;
    }
    public Task<Author> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        return _repository.GetByIdAsync(request.Id);
    }
}
