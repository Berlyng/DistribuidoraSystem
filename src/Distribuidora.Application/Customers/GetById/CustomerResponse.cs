using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Application.Customers.GetById
{
    public sealed record CustomerResponse(
        Guid Id,
        string Name,
        string TaxId,
        string PhoneNumber,
        string Address,
        string? ContactName,
        bool CreditEnabled,
        int CreditDays,
        bool IsActive);
    
}
