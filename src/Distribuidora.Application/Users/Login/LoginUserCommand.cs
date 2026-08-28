using Distribuidora.API.Users.Login;
using Distribuidora.Domain.Common;
using MediatR;

namespace Distribuidora.Application.Users.Login
{
    public sealed record LoginUserCommand(
        string Email, 
        string Password) : IRequest<Result<LoginResult>>;
    
}
