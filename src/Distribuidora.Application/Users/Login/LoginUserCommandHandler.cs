using Distribuidora.API.Users.Login;
using Distribuidora.Application.Users.Abstractions;
using Distribuidora.Domain.Common;
using Distribuidora.Domain.Users;
using Distribuidora.Domain.Users.ValueObject;
using MediatR;

namespace Distribuidora.Application.Users.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<LoginResult>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        public LoginUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public async Task<Result<LoginResult>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var emailResult = Email.Create(request.Email);
            if (emailResult.IsFailure)
            {
                return Result<LoginResult>.Failure(UserErrors.InvalidCredentials);
            }

            var passwordResult = Password.Create(request.Password);
            if (passwordResult.IsFailure)
            {
                return Result<LoginResult>.Failure(UserErrors.InvalidCredentials);
            }



            var user = await _userRepository.GetByEmailAsync(emailResult.Value, cancellationToken);

            if (user is null)
            {
                return Result<LoginResult>.Failure(UserErrors.InvalidCredentials);
            }

            if(user.Status == UserStatus.Blocked)
            {
                return Result<LoginResult>.Failure(UserErrors.Blocked);
            }
            
            if(user.Status == UserStatus.Suspended)
            {
                return Result<LoginResult>.Failure(UserErrors.Suspended);
            }

            var accessToken = _jwtProvider.GenerateAccessToken(user);

            var passwordIsValid = _passwordHasher.Verify(passwordResult.Value, user.PasswordHash);

            if(!passwordIsValid)
            {
                return Result<LoginResult>.Failure(UserErrors.InvalidCredentials);
            }

            var response = new LoginResult(user.Id, user.Name.FirstName, user.Name.LastName, user.Email.Value,user.Role.ToString(), accessToken);

            return Result<LoginResult>.Success(response);
        }
    }
}
