using Distribuidora.Domain.Customers;
using Distribuidora.Domain.Customers.Value_Object;

namespace Distribuidora.Application.Customers.Abstractions
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Customer?> GetTaxIdAsync(TaxId taxId, CancellationToken cancellationToken = default);
        Task<bool> ExistsByTaxIdAsync(TaxId taxId, CancellationToken cancellationToken = default);

        Task AddAsync(Customer customer, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
