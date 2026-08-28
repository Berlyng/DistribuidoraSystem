using Distribuidora.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Application.Users.Abstractions
{
    public interface IJwtProvider
    {
        string GenerateAccessToken(User user);
    }
}
