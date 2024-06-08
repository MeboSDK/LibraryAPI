using Application.Commands.BookC.Commands.Records;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.BookC.Commands;

public record UpdateBookCommand(
    int Id,
    [Required] string Title,
    string Descrption,
    double Rate,
    int[] AuthorsIds,
    DateTime ReleaseDate,
    [Required] int TotalCount,
    int CurrentCount) : IRequest;

