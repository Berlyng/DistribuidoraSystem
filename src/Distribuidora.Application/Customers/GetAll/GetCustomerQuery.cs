using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Application.Customers.GetAll
{
    public sealed record GetCustomerQuery(
        string? Search,
        bool? IsActive
        ) : IRequest<IReadOnlyList<CustomerListItemResponse>>;
    
}
