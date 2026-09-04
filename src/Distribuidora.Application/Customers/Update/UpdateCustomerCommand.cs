using Distribuidora.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Application.Customers.Update
{
    public sealed record UpdateCustomerCommand(
        Guid customerId,
        string Name,
        string TaxId,
        string PhoneNumber,
        string Address,
        string? ContactName,
        bool CreditEnable,
        int CreditDays
        ) : IRequest<Result>;


}
