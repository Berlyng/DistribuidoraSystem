using Distribuidora.Domain.Common;
using MediatR;

namespace Distribuidora.API.Users.Login
{
    public sealed record LoginUserCommand(
        string Email, 
        string Password) : IRequest<Result<LoginResult>>;
    
}
