using Distribuidora.Application.Customers.Abstractions;
using Distribuidora.Domain.Customers;
using Distribuidora.Domain.Customers.Value_Object;
using Microsoft.EntityFrameworkCore;

namespace Distribuidora.Infrastructure.Persistence.Repositories
{
    public sealed class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CustomerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Customer customer, CancellationToken cancellationToken = default)
        {
            await _dbContext.Customers.AddAsync(customer, cancellationToken);
        }

        public async Task<bool> ExistsByTaxIdAsync(TaxId taxId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Customers.AnyAsync(c => c.TaxId == taxId, cancellationToken);
        }

        public async Task<IReadOnlyList<Customer>> GetAllAsync(
    string? search,
    bool? isActive,
    CancellationToken cancellationToken = default)
        {
            IQueryable<Customer> query = _dbContext.Customers
                .AsNoTracking();

            if (isActive.HasValue)
            {
                query = query.Where(customer =>
                    customer.IsActive == isActive.Value);
            }

            var customers = await query
                .ToListAsync(cancellationToken);

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();

                customers = customers
                    .Where(customer =>
                        customer.Name.Value.Contains(
                            search,
                            StringComparison.OrdinalIgnoreCase) ||
                        customer.TaxId.Value.Contains(
                            search,
                            StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return customers
                .OrderBy(customer => customer.Name.Value)
                .ToList();
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
