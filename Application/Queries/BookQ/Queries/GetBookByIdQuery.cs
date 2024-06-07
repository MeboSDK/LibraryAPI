using Domain.Entities;
using MediatR;

namespace Application.Queries.BookQ.Queries;
public record GetBookByIdQuery(int Id) : IRequest<Book>;
