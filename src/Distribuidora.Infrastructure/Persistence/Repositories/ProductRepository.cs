using Distribuidora.Application.Products.Abtractions;
using Distribuidora.Domain.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Infrastructure.Persistence.Repositories
{
    public sealed class ProductRepository : IProductRepository
    {

        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
        {
            await _context.Products.AddAsync(product, cancellationToken);
        }

        public Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return _context.Products.AnyAsync(p => p.Name == name, cancellationToken);
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync(string? search, bool? active, CancellationToken cancellationToken = default)
        {
            IQueryable<Product> query = _context.Products.Include(p => p.Presentaciones).AsNoTracking();
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => p.Name.Contains(search));
            }
            if (active.HasValue)
            {
                query = query.Where(p => p.IsActive == active.Value);
            }
            
            return await query.OrderBy(p => p.Name).ToListAsync(cancellationToken);
        }

        public async Task<Product> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Products.Include(p => p.Presentaciones)
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }
        

        public async Task<Product> GetbyNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Products.Include(p => p.Presentaciones)
                .FirstOrDefaultAsync(p => p.Name == name, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
           await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
