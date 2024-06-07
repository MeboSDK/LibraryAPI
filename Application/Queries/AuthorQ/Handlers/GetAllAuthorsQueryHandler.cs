using Application.Queries.AuthorQ.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Queries.AuthorQ.Handlers;

public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, IEnumerable<Author>>
{
    private readonly IRepository<Author> _repository;
    public GetAllAuthorsQueryHandler(IRepository<Author> repository)
    {
        _repository = repository;
    }
    public Task<IEnumerable<Author>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
    {
        return _repository.GetAllAsync(request.filter);
    }
}