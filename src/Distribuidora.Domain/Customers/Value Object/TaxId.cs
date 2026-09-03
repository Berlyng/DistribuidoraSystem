using Distribuidora.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Domain.Customers.Value_Object
{
    public sealed record TaxId
    {
        public TaxId(string value)
        {
            Value = value;
        }

        public string Value { get; }


        public static Result<TaxId> Create(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return Result<TaxId>.Failure(new Error("TaxId.Required", "La cedula o RNC es obligatoria"));

            }
            var normalized = value
                .Replace("-", string.Empty)
                .Replace(" ", string.Empty)
                .Trim();

            if (!normalized.All(char.IsDigit))
            {
                return Result<TaxId>.Failure(new Error("Customer.TaxIdInvalidFormat", "La cedula o RNC debe contener solo números"));
            }

            if (normalized.Length != 9 && normalized.Length != 11)
            {
                return Result<TaxId>.Failure(new Error("Customer.TaxIdInvalidLength", "La cedula o RNC debe tener 9 o 11 dígitos"));
            }
            return Result<TaxId>.Success(new TaxId(normalized));
        }

        public override string ToString() => Value;
    }
}
