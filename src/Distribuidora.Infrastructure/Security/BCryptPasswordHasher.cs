using Distribuidora.Application.Users.Abstractions;
using Distribuidora.Domain.Users.ValueObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Infrastructure.Security
{
    public sealed class BCryptPasswordHasher : IPasswordHasher
    {
        public PasswordHash Hash(Password password)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(password.Value);
            return PasswordHash.Create(hash).Value;
        }

        public bool Verify(Password password, PasswordHash hash)
        {
            return BCrypt.Net.BCrypt.Verify(password.Value, hash.Value);
        }
    }
}
