using Distribuidora.Application.Customers.Abstractions;
using Distribuidora.Domain.Common;
using Distribuidora.Domain.Customers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Application.ChangeStatus
{
    public sealed class ActivateCustomerCommandHandler : IRequestHandler<ActivateCustomerCommand, Result>
    {
        private readonly ICustomerRepository _customerRepository;

        public ActivateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Result> Handle(ActivateCustomerCommand request, CancellationToken cancellationToken)
        {
           var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
            if (customer == null)
            {
                return Result.Failure(CustomerErrors.NotFound);
            }
            customer.Activate();
            await _customerRepository.SaveChangesAsync();
            return Result.Success();
        }
    }
}
