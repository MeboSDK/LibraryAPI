using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.BookC.Commands;

public record TakeOutBookCommand(
     string UsedId,
     int BookId,
     int Count) : IRequest;
