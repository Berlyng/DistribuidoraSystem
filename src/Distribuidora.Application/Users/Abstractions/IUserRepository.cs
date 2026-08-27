using Distribuidora.Domain.Users;
using Distribuidora.Domain.Users.ValueObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Application.Users.Abstractions
{
    public interface IUserRepository
    {
        Task<bool> ExistsByEmailAsync(Email email, CancellationToken cancellationToken = default);
        Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default);
        Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task AddAsync(User user, CancellationToken cancellationToken = default);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
