using IdentityApi.Data.Repositories.Contracts;
using IdentityApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<User> Get()
            => db.User;

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
