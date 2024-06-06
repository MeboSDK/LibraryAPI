using Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;
using Application.Commands.UserC.Commands;
using FluentValidation;

namespace Application.Commands.UserC.Handlers;
public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, string>
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly IValidator<User> _userValidator;

    public UserRegisterCommandHandler(UserManager<User> userManager,
                                        IUnitOfWork unitOfWork,
                                        ITokenService tokenService,
                                        IValidator<User> userValidator)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
        _userValidator = userValidator;
    }

    public async Task<string> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
        var user = new User { UserName = request.UserName, Email = request.Email };

        var validationResult = await _userValidator.ValidateAsync(user);

        if (!validationResult.IsValid)
            throw new InvalidDataException(string.Join(", ", validationResult.Errors));

        var result = await _userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "User");
            await _unitOfWork.CommitAsync();
            return await _tokenService.GenerateTokenAsync(user, await _userManager.GetRolesAsync(user));
        }
        throw new ArgumentException(string.Join(", ", result.Errors.Select(e => e.Description)));
    }
}
