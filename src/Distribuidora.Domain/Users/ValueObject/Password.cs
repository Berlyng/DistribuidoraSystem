using Distribuidora.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Domain.Users.ValueObject
{
    public sealed record Password
    {
        public Password(string value)
        {
            Value = value;
        }

        public string Value { get;}

        public static Result<Password> Create(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return Result<Password>.Failure(new Error("Password.Required", "La contraseña es requerida"));
            }
            if(value.Length < 8)
            {
                return Result<Password>.Failure(new Error("Password.TooShort", "La contraseña es muy corta"));
            }
            if(value.Length > 100)
            {
                return Result<Password>.Failure(new Error("Password.TooLong", "La contraseña es muy larga"));

            }
            if (!value.Any(char.IsUpper))
            {
                return Result<Password>.Failure(new Error("Password.UpperCaseRequired", "La contraseña debe tener al menos un caracter en mayuscula"));
            }
            if(!value.Any(char.IsLower))
            {
                return Result<Password>.Failure(new Error("Password.LowerCaseRequired", "La contraseña debe tener al menos un caracter en miniscula"));
            }
            if(!value.Any(char.IsDigit))
            {
                return Result<Password>.Failure(new Error("Password.DigitRequired", "La contraseña debe tener al menos un caracter especial"));
            }

            return Result<Password>.Success(new Password(value));

        }

        public override string ToString() => "********";
    }
}
