using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Data.Repositories.Contracts;
using UserApi.Models;

namespace UserApi.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        ProjectManagerContext db;

        public UserRepository(ProjectManagerContext _db)
        {
            db = _db;
        }

        public async Task<User> CreateAsync(User user)
        {
            await db.User.AddAsync(user);
            await db.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            db.User.Update(user);
            await db.SaveChangesAsync();
            return user;
        }

        public IEnumerable<User> Get()
            => db.User;

        public IEnumerable<User> Get(string userName)
            => db.User.Where(c => c.Name.Contains(userName));

        public async Task<User> GetAsync(int userId)
            => await db.User.Where(c => c.Id == userId).FirstAsync();


        public async Task DeleteAsync(int userId)
        {
            User user = await GetAsync(userId);
            db.User.Remove(user);
            await db.SaveChangesAsync();
        }
    }
}
