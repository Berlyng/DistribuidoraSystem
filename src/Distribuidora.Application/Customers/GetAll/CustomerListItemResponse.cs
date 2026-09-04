using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Application.Customers.GetAll
{
    public sealed record CustomerListItemResponse(
        Guid Id,
        string Name,
        string TaxId,
        string PhoneNumber,
        bool CreditEnabled,
        int CreditDays,
        bool IsActive
    );

}
