using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.UserC.Commands;

public record UserRegisterCommand(
    [Required] string UserName,
    [Required] string Email,
    [Required] [DataType(DataType.Password)] string Password
) : IRequest<string>;

