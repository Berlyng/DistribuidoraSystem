using Distribuidora.Application.Users.Abstractions;
using Distribuidora.Domain.Common;
using Distribuidora.Domain.Users;
using Distribuidora.Domain.Users.ValueObject;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Application.Users.Register
{
    public sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<Guid>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _hasher;

        public RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher hasher)
        {
            _userRepository = userRepository;
            _hasher = hasher;
        }

        public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var nameResult = PersonName.Create(request.FirstName, request.LastName);
            if(nameResult.IsFailure)
            {
                return Result<Guid>.Failure(nameResult.Error);
            }

            var emailResult = Email.Create(request.Email);
            if(emailResult.IsFailure)
            {
                return Result<Guid>.Failure(emailResult.Error);
            }

            var emailExists = await _userRepository.ExistsByEmailAsync(emailResult.Value);
            if(emailExists)
            {
                return Result<Guid>.Failure(UserErrors.EmailAlreadyExists);
            }

            var passwordResult = Password.Create(request.Password);
            if (passwordResult.IsFailure)
            {
                return Result<Guid>.Failure(passwordResult.Error);
            }

            var passwordHash = _hasher.Hash(passwordResult.Value);
            var userResult =  User.Create(nameResult.Value, emailResult.Value, passwordHash, request.Role);

            if (userResult.IsFailure)
            {
                return Result<Guid>.Failure(userResult.Error);
            }

            var user = userResult.Value;
            await _userRepository.AddAsync(user, cancellationToken);
            await _userRepository.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(user.Id);
        }
    }
}
