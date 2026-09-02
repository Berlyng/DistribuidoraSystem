using Distribuidora.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Domain.Customers.Value_Object
{
    public sealed record CustomerName
    {
        public CustomerName(string value)
        {
            Value = value;
        }

        public string Value { get; }


        public static Result<CustomerName> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Result<CustomerName>.Failure(new Error("Customer.Required", "El nombre del cliente es obligatorio"));
            }

            value = value.Trim();

            if (value.Length > 150)
            {
                return Result<CustomerName>.Failure(new Error ("Customer.NameTooLong", "El nombre del cliente no puede exceder los 150 caracteres"));
            }
            return Result<CustomerName>.Success(new CustomerName(value));

        }

        public override string ToString() => Value;
    }
}
