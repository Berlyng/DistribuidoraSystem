using Distribuidora.Domain.Users.ValueObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Application.Users.Abstractions
{
    public interface IPasswordHasher
    {
        PasswordHash Hash(Password password);

        bool Verify(Password password, PasswordHash hash);
    }
}
