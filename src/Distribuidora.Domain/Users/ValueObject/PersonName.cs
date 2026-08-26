using Distribuidora.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Domain.Users.ValueObject
{
    public sealed record PersonName
    {

        public string FirstName { get; }
        public string LastName { get; }
        private PersonName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public static Result<PersonName> Create(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                return Result<PersonName>.Failure(new Error("PersonName.FirstNameRequired", "El nombre es requerido"));
            }
            if (string.IsNullOrEmpty(lastName))
            {
                return Result<PersonName>.Failure(new Error("PersonName.LastNamewRequired", "El apellido es requerido"));
            }

            firstName.Trim();
            lastName.Trim();


            if(firstName.Length > 100)
            {
                return Result<PersonName>.Failure(new Error("PersonName.FirstNameTooLong", "El nombre es demasiado largo"));
            }

            if (lastName.Length > 100)
            {
                return Result<PersonName>.Failure(new Error("PersonNamew.LastNamewTooLong", "El apellido es demasiado largo"));
            }

            return Result<PersonName>.Success(new  PersonName(firstName, lastName));
        }


    }
}
