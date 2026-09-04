using Distribuidora.Application.Customers.Abstractions;
using Distribuidora.Domain.Common;
using Distribuidora.Domain.Customers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Application.ChangeStatus
{
    public sealed class DeactivateCustomerCommandHandler : IRequestHandler<DeactivateCustomerCommand, Result>
    {
        private readonly ICustomerRepository _customerRepository;

        public DeactivateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Result> Handle(DeactivateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);
            if (customer == null)
            {
                return Result.Failure(CustomerErrors.NotFound);
            }
            customer.Deactivate();
            await _customerRepository.SaveChangesAsync();
            return Result.Success();
        }
    }
}
