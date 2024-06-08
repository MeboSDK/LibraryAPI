using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.BookC.Commands;

public record TakeOutBookCommand(
    [Required] string UsedId,
    [Required] int BookId,
    [Required] int Count) : IRequest;
