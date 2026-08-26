using Distribuidora.Domain.Common;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Distribuidora.Domain.Users.ValueObject
{
    public sealed record Email
    {
        public string Value { get; }

        private Email(string value)
        {
            Value = value;
        }

        public static Result<Email> Create(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return Result<Email>.Failure(new Error("Email.Required", "El email es requerido"));
            }

            value = value.Trim().ToLowerInvariant();

            if (value.Length > 150)
            {
                return Result<Email>.Failure(new Error("Email.TooLong", "El email no puede tener mas 150 caracteres"));
            }

            if (!IsValid(value))
            {
                return Result<Email>.Failure(new Error("Email.Invalid", "El formta es invalido"));
            }

            return Result<Email>.Success(new Email(value));

        }

        public static Email FromPersistence(string value)
        {
            return new Email(value);
        }

        private static bool IsValid(string value)
        {
           try
            {
                var address = new MailAddress(value);
                return address.Address == value;
            }
            catch
            {
                return false;
            }
        }

        public override string ToString() => Value;
    }
}
