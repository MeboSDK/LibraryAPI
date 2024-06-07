using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.AuthorC.Commands;

public record UpdateAuthorCommand(
    [Required] int Id,
    [Required] string FirstName,
    [Required] string LastName,
    [Required] DateTime DateOfBirth) : IRequest;

