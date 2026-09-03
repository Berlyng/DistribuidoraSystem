using Distribuidora.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Domain.Customers
{
    public static class CustomerErrors
    {
        public static readonly Error NotFound = new Error("Customer.NotFound", "Cliente no encontrado");
        public static readonly Error TaxIdAlreadyExists = new Error("Customer.TaxIdAlreadyExists", "Ya existe un cliente con ese RNC o CEDULA");
        public static readonly Error AddressRequired = new Error("Customer.AddressRequired", "La dirección es requerida");
        public static readonly Error AddressTooLong = new Error("Customer.AddressTooLong", "La dirección no puede tener más de 250 caracteres");
        public static readonly Error ContactNameTooLong = new Error("Customer.ContactNameTooLong", "El nombre del contacto no puede tener más de 150 caracteres"); 
        public static readonly Error InvalidCreditDays = new Error("Customer.InvalidCreditDays", "Los días de crédito deben estar entre 1 y 365.");
        public static readonly Error Inactive = new Error("Customer.Inactive", "El cliente está inactivo y no puede realizar operaciones.");

    }
}
