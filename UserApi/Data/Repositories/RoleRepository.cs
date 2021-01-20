using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Role>> GetAsync()
            => await db.Role.ToListAsync();

        public async Task<Role> GetAsync(Guid roleId)
            => await db.Role.FirstAsync(c => c.Id == roleId);

        public async Task DeleteAsync(Guid roleId)
        {
            Role role = await GetAsync(roleId);
            db.Role.Remove(role);
            await db.SaveChangesAsync();
        }
    }
}
