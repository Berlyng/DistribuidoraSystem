using Distribuidora.Domain.Common;
using Distribuidora.Domain.Customers.Value_Object;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Domain.Customers
{
    public sealed class Customer : BaseEntity
    {
        public Customer()
        {
        }

        public Customer(CustomerName name, TaxId taxId, PhoneNumber phoneNumber, string address, string? contactName, bool creditEnable, int creditDays)
        {
            Name = name;
            TaxId = taxId;
            PhoneNumber = phoneNumber;
            Address = address;
            ContactName = contactName;
            CreditEnable = creditEnable;
            CreditDays = creditEnable ? creditDays : 0;
            IsActive = true;
        }

        public CustomerName Name { get; private set; } = null!;
        public TaxId TaxId { get; private set; } = null!;
        public PhoneNumber PhoneNumber { get; private set; } = null!;
        public string Address { get; private set; } = string.Empty;

        public string? ContactName { get; private set; }
        public bool CreditEnable { get; private set; }
        public int CreditDays { get; private set; }
        public bool IsActive { get; private set; }


        public static Result<Customer> Create(CustomerName name, TaxId taxId, PhoneNumber phoneNumber, string address, string? contactName, bool creditEnable, int creditDays = 30)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                return Result<Customer>.Failure(CustomerErrors.AddressRequired);
            }
            address = address.Trim();
            if (address.Length > 250)
            {
                return Result<Customer>.Failure(CustomerErrors.AddressTooLong);
            }
            if (!string.IsNullOrWhiteSpace(contactName) && contactName!.Trim().Length > 150)
            {
                return Result<Customer>.Failure(CustomerErrors.ContactNameTooLong);
            }

            if (creditEnable && (creditDays <= 0 || creditDays > 365))
            {
                return Result<Customer>.Failure(CustomerErrors.InvalidCreditDays);
            }

            var customer = new Customer(name, taxId, phoneNumber, address, contactName?.Trim(), creditEnable, creditDays);
            return Result<Customer>.Success(customer);

        }

        public Result EnableCredit(int creditDays = 30)
        {
            if (creditDays <= 0 || creditDays > 365)
            {
                return Result.Failure(CustomerErrors.InvalidCreditDays);
            }
            CreditEnable = true;
            CreditDays = creditDays;
            return Result.Success();
        }

        public Result DisableCredit()
        {
            CreditEnable = false;
            CreditDays = 0;
            return Result.Success();
        }

        public Result Activate()
        {
            IsActive = true;
            return Result.Success();
        }

        public Result Deactivate()
        {
            IsActive = false;
            return Result.Success();

        }
    }
}
