using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.AuthorC.Commands;

public record AddAuthorCommand(
    [Required] string FirstName,
    [Required] string LastName,
    [Required] DateTime DateOfBirth)
    : IRequest;

