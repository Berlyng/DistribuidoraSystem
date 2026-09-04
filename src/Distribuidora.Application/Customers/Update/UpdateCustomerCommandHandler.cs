using Distribuidora.Application.Customers.Abstractions;
using Distribuidora.Domain.Common;
using Distribuidora.Domain.Customers;
using Distribuidora.Domain.Customers.Value_Object;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Application.Customers.Update
{
    public sealed class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Result>
    {
        private readonly ICustomerRepository _customerRepository;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Result> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.customerId, cancellationToken);
            if(customer is null)
            {
                return Result.Failure(CustomerErrors.NotFound);
            }

            var nameResult = CustomerName.Create(request.Name);
            if (nameResult.IsFailure)
            {
                return Result.Failure(nameResult.Error);
            }

            var taxIdResult = TaxId.Create(request.TaxId);
            if (taxIdResult.IsFailure)
            {
                return Result.Failure(taxIdResult.Error);
            }

            var phoneNumberResult = PhoneNumber.Create(request.PhoneNumber);
            if (phoneNumberResult.IsFailure)
            {
                return Result.Failure(phoneNumberResult.Error);
            }

            var existingCustomerWithSameTaxId = await _customerRepository.GetTaxIdAsync(taxIdResult.Value, cancellationToken);
            if (existingCustomerWithSameTaxId is not null && existingCustomerWithSameTaxId.Id != customer.Id)
            {
                return Result.Failure(CustomerErrors.TaxIdAlreadyExists);
            }

            var updateResult = customer.Update(nameResult.Value, taxIdResult.Value, phoneNumberResult.Value, request.Address, request.ContactName);
            if (updateResult.IsFailure)
            {
                return Result.Failure(updateResult.Error);
            }
            await _customerRepository.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
