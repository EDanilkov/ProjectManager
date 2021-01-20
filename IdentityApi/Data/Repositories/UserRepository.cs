using IdentityApi.Data.Models;
using IdentityApi.Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IdentityApi.Data.Repositories
{
    public class UserRepository : IUserRepository
    {

        ProjectManagerContext db;

        public UserRepository(ProjectManagerContext _db)
        {
            db = _db;
        }

        public async Task<IEnumerable<User>> GetAsync()
            => await db.User.ToListAsync();

        public async Task<User> FirstOrDefault(Expression<Func<User, bool>> predicate = null)
            => await db.User.FirstOrDefaultAsync(predicate);

        public async Task<User> CreateAsync(User user)
        {
            user.RefreshTokenExpiryTime = DateTime.UtcNow;
            await db.User.AddAsync(user);
            await db.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            await db.SaveChangesAsync();
            return user;
        }
    }
}
