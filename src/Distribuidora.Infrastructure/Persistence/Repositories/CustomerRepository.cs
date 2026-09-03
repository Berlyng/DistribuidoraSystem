using Distribuidora.Application.Customers.Abstractions;
using Distribuidora.Domain.Customers;
using Distribuidora.Domain.Customers.Value_Object;
using Microsoft.EntityFrameworkCore;

namespace Distribuidora.Infrastructure.Persistence.Repositories
{
    public sealed class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public async Task AddAsync(Customer customer, CancellationToken cancellationToken = default)
        {
            await _dbContext.Customers.AddAsync(customer, cancellationToken);
        }

        public async Task<bool> ExistsByTaxIdAsync(TaxId taxId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Customers.AnyAsync(c => c.TaxId == taxId, cancellationToken);
        }

        public async Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }
        

        public async Task<Customer?> GetTaxIdAsync(TaxId taxId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(c => c.TaxId == taxId, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
