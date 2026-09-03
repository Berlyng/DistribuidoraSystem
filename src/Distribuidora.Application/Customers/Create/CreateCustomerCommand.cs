using Distribuidora.Domain.Common;
using MediatR;

namespace Distribuidora.Application.Customers.Create
{
    public sealed record CreateCustomerCommand(
        string Name,
        string TaxId,
        string PhoneNumber,
        string Address,
        string? ContactName,
        bool CreditEnabled,
        int CreditDays) : IRequest<Result<Guid>>;
   
}
