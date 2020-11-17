using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Data.Repositories.Contracts;
using UserApi.Models;

namespace UserApi.Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        ProjectManagerContext db;

        public RoleRepository(ProjectManagerContext _db)
        {
            db = _db;
        }

        public async Task<Role> CreateAsync(Role role)
        {
            await db.AddAsync(role);
            await db.SaveChangesAsync();
            return role;
        }

        public IEnumerable<Role> Get()
            => db.Role;

        public async Task<Role> GetAsync(int roleId)
            => await db.Role.FirstAsync(c => c.Id == roleId);

        /*public async Task<Role> GetRoleByUserIdAsync(int userId)
        {
            User user = await db.User.FirstAsync(user => user.Id == userId);
            return await db.Role.FirstAsync(role => role.Id == user.RoleId);
        }*/

        public async Task DeleteAsync(int roleId)
        {
            Role role = await GetAsync(roleId);
            db.Role.Remove(role);
            await db.SaveChangesAsync();
        }
    }
}
