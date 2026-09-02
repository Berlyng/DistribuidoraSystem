using Distribuidora.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Domain.Customers.Value_Object
{
    public sealed record PhoneNumber
    {
        public PhoneNumber(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static Result<PhoneNumber> Create(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return Result<PhoneNumber>.Failure(new Error("Customer.PhoneRequired" ,"El numero de telefono es requerido"));
            }

            var normalized = value
                .Replace("-", string.Empty)
                .Replace("(", string.Empty)
                .Replace(")", string.Empty)
                .Replace(" ", string.Empty)
                .Trim();

            if (!normalized.All(char.IsDigit))
            {
                return Result<PhoneNumber>.Failure(new Error("Customer.InvalidPhone", "El numero de telefono solo puede contener numeroes"));
            }

            if(normalized.Length != 10)
            {
                return Result<PhoneNumber>.Failure(new Error("Customer.InvalidPhone", "El numero de telefono debe tener 10 digitos"));
            }

            return Result<PhoneNumber>.Success(new PhoneNumber(normalized));
        }


        public override string ToString() => Value;

    }
}
