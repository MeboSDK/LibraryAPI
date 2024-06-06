using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.UserC.Commands;

public record UserRegisterCommand(
    [Required] string UserName,
    [Required] string Email,
    [Required] string Password,
    [Required][DataType(DataType.Password)] string Role
) : IRequest<string>;

