using Distribuidora.Application.Users.Abstractions;
using Distribuidora.Domain.Users;
using Distribuidora.Domain.Users.ValueObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Infrastructure.Persistence.Repositories
{
   
    public sealed class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        {
             await _context.Users.AddAsync(user, cancellationToken);
        }

        public async Task<bool> ExistsByEmailAsync(Email email, CancellationToken cancellationToken = default)
        {
           return await _context.Users.AnyAsync(user => user.Email.Value == email.Value, cancellationToken);
        }

        public async Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Email.Value == email.Value, cancellationToken);
        }

        public async Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
