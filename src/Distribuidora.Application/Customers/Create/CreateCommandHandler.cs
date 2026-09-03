using Distribuidora.Application.Customers.Abstractions;
using Distribuidora.Domain.Common;
using Distribuidora.Domain.Customers;
using Distribuidora.Domain.Customers.Value_Object;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Application.Customers.Create
{
    public sealed class CreateCommandHandler : IRequestHandler<CreateCustomerCommand, Result<Guid>>
    {
        public readonly ICustomerRepository _customerRepository;
        public async Task<Result<Guid>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var nameResult = CustomerName.Create(request.Name);
            if (nameResult.IsFailure)
            {
                return Result<Guid>.Failure(nameResult.Error);
            }
            var taxIdResult = TaxId.Create(request.TaxId);
            if (taxIdResult.IsFailure)
            {
                return Result<Guid>.Failure(taxIdResult.Error);
            }
            var phoneNumberResult = PhoneNumber.Create(request.PhoneNumber);
            if (phoneNumberResult.IsFailure)
            {
                return Result<Guid>.Failure(phoneNumberResult.Error);
            }
            
            var taxIdAlreadyExists =  await _customerRepository.ExistsByTaxIdAsync(taxIdResult.Value, cancellationToken);

            if (taxIdAlreadyExists)
            {
                return Result<Guid>.Failure(CustomerErrors.TaxIdAlreadyExists);
            }

            var customerResult = Customer.Create(
                nameResult.Value, 
                taxIdResult.Value, 
                phoneNumberResult.Value, 
                request.Address, 
                request.ContactName, 
                request.CreditEnabled, 
                request.CreditDays);

            if (customerResult.IsFailure)
            {
                return Result<Guid>.Failure(customerResult.Error);
            }

            await _customerRepository.AddAsync(customerResult.Value, cancellationToken);
            await _customerRepository.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(customerResult.Value.Id);
        }
    }
}
