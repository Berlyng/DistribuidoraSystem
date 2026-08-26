using Distribuidora.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Domain.Users.ValueObject
{
    public sealed record PasswordHash
    {
        public PasswordHash(string value)
        {
            Value = value;
        }

        public string Value { get; }

       public static Result<PasswordHash> Create(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return Result<PasswordHash>.Failure(new Error("PasswordHash.Required", "El hash no puede estar vacio"));

            }

            return Result<PasswordHash>.Success(new PasswordHash(value));
        }

        public static PasswordHash FromPersistence(string value)
        {
            return new PasswordHash(value);
        }

        public override string ToString() => "********";
    }
}
