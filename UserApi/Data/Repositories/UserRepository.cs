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
            user.RefreshTokenExpiryTime = DateTime.Now;
            await db.User.AddAsync(user);
            await db.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            await db.SaveChangesAsync();
            return user;
        }

        public IEnumerable<User> Get()
            => db.User;

        public IEnumerable<User> Get(string userName)
            => db.User.Where(c => c.Username.Contains(userName));

        public async Task<User> GetAsync(int userId)
            => await db.User.FirstAsync(c => c.Id == userId);

        public async Task<User> GetAsync(string username)
            => await db.User.FirstAsync(c => c.Username == username);

        public async Task DeleteAsync(int userId)
        {
            User user = await GetAsync(userId);
            db.User.Remove(user);
            await db.SaveChangesAsync();
        }

        public async Task AddPhoto(string base64ImageRepresentation, int userId)
        {
            User user = await db.User.FirstAsync(c => c.Id == userId);
            user.PhotoBase64 = base64ImageRepresentation;
            await db.SaveChangesAsync();
        }

        public async Task DeletePhoto(int userId)
        {
            User user = await db.User.FirstAsync(c => c.Id == userId);
            user.PhotoBase64 = null;
            await db.SaveChangesAsync();
        }
    }
}
