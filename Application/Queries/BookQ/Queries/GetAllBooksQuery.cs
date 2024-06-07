using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.Queries.BookQ.Queries;
public record GetAllBooksQuery(Expression<Func<Book, bool>> filter = null) : IRequest<IEnumerable<Book>>;
