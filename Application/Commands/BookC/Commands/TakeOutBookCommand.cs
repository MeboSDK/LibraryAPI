using MediatR;

namespace Application.Commands.BookC.Commands;

public record TakeOutBookCommand(string UsedId, int BookId,int Count) : IRequest;
