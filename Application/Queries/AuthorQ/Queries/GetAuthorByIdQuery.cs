using Domain.Entities;
using MediatR;

namespace Application.Queries.AuthorQ.Queries;
public record GetAuthorByIdQuery(int Id) : IRequest<Author>;
