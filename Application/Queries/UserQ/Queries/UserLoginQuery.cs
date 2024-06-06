using MediatR;

namespace Application.Queries.UserQ.Queries;

public record UserLoginQuery(string Email,string Password) : IRequest<string>;
