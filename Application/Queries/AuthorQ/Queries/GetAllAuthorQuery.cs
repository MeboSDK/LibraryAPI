using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.Queries.AuthorQ.Queries;
public record GetAllAuthorsQuery(Expression<Func<Author,bool>> filter = null) : IRequest<IEnumerable<Author>>;
