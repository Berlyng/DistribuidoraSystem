using Distribuidora.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Application.Customers.GetById
{
    public sealed record GetCustomerByIdQuery(
        Guid CustomerId
        ) : IRequest<Result<CustomerResponse>>;


}
