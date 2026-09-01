using Distribuidora.Domain.Common;
using Distribuidora.Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Application.Users.Register
{
    public sealed record RegisterUserCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password,
        UserRole Role) : IRequest<Result<Guid>>;
    
}
