using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            db.Update(user);
            await db.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<User>> GetAsync()
            => await db.User.ToListAsync();

        public async Task<IEnumerable<User>> GetAsync(string userName)
            => await db.User.Where(c => c.Username.Contains(userName)).ToListAsync();

        public async Task<User> FirstOrDefaultAsync(Expression<Func<User, bool>> predicate = null)
            => await db.User.FirstOrDefaultAsync(predicate);

        public async Task DeleteAsync(Guid userId)
        {
            User user = await FirstOrDefaultAsync(u => string.Equals(userId, u.Id));
            db.User.Remove(user);
            await db.SaveChangesAsync();
        }

        public async Task AddPhotoAsync(string base64ImageRepresentation, Guid userId)
        {
            User user = await db.User.FirstAsync(c => c.Id == userId);
            user.PhotoBase64 = base64ImageRepresentation;
            await db.SaveChangesAsync();
        }

        public async Task DeletePhotoAsync(Guid userId)
        {
            User user = await db.User.FirstAsync(c => c.Id == userId);
            user.PhotoBase64 = null;
            await db.SaveChangesAsync();
        }
    }
}
