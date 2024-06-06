using Application.Queries.UserQ.Queries;
using Domain.Abstractions;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace Application.Queries.UserQ.Handlers
{
    public class UserLoginQueryHandler : IRequestHandler<UserLoginQuery, string>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;
        public UserLoginQueryHandler(UserManager<User> userManager,
                           SignInManager<User> signInManager,
                           ITokenService tokenService, IUnitOfWork unitOfWork,
                           IValidator<User> validator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<string> Handle(UserLoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new InvalidDataException("Invalid credentials");
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, false);
            if (result.Succeeded)
            {
                return await _tokenService.GenerateTokenAsync(user, await _userManager.GetRolesAsync(user));
            }

            throw new InvalidDataException("Invalid credentials");
        }
    }
}
