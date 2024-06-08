using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.BookC.Commands;

public record ReturnBookCommand([Required]int Id, [Required] int Count) : IRequest;
