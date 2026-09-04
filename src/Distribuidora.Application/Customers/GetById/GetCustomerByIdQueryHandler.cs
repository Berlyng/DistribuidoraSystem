using Distribuidora.Application.Customers.Abstractions;
using Distribuidora.Domain.Common;
using Distribuidora.Domain.Customers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Application.Customers.GetById
{
    public sealed class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Result<CustomerResponse>>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Result<CustomerResponse>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId);

            if (customer is null)
            {
                return Result<CustomerResponse>.Failure(CustomerErrors.NotFound);

            }

            var response = new CustomerResponse(
                customer.Id,
                customer.Name.Value,
                customer.TaxId.Value,
                customer.PhoneNumber.Value,
                customer.Address,
                customer.ContactName,
                customer.CreditEnable,
                customer.CreditDays,
                customer.IsActive
                );
            
            return Result<CustomerResponse>.Success(response);
        }
    }
}
