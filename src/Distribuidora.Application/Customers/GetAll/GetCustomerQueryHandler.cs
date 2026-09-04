using Distribuidora.Application.Customers.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Application.Customers.GetAll
{
    public sealed class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, IReadOnlyList<CustomerListItemResponse>>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IReadOnlyList<CustomerListItemResponse>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var customers = await _customerRepository.GetAllAsync(request.Search, request.IsActive, cancellationToken);
            return customers.Select(customer => new CustomerListItemResponse(
                customer.Id,
                customer.Name.Value,
                customer.TaxId.Value,
                customer.PhoneNumber.Value,
                customer.CreditEnable,
                customer.CreditDays,
                customer.IsActive
            )).ToList();
        }
    }
}
