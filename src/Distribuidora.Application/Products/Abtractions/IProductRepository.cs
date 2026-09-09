using Distribuidora.Domain.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Application.Products.Abtractions
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(Guid id, CancellationToken cancellationToken = default );
        Task<Product> GetbyNameAsync(string name, CancellationToken cancellationToken = default);
        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Product>> GetAllAsync(string? search, bool? active, CancellationToken cancellationToken = default);
        Task AddAsync(Product product, CancellationToken cancellationToken = default);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
