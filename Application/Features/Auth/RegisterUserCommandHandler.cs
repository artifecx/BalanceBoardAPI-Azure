using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Mediator;
using Application.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth
{
    public class RegisterUserCommandHandler(IApplicationDbContext context) : IRequestHandler<RegisterUserCommand, Result<UserDto>>
    {
        public async Task<Result<UserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var newUser = request.Request;

            if (await context.Users.AnyAsync(u => u.Email.ToLower() == newUser.Email.ToLower(), cancellationToken))
            {
                return Result<UserDto>.Failure("Email is already in use.");
            }

            var user = new User();
            user.Email = newUser.Email.ToLower();
            user.Username = newUser.Email.Split('@')[0] ?? newUser.Email; 
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, newUser.Password);
            user.CreatedAt = DateTime.UtcNow;

            context.Users.Add(user);
            await context.SaveChangesAsync(cancellationToken);

            var userDto = new UserDto
            (
                user.Username,
                user.Email,
                user.CreatedAt
            );

            return Result<UserDto>.Success(userDto);
        }
    }
}
