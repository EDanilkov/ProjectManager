using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Models;

namespace UserApi.Data.Repositories.Contracts
{
    interface IRoleRepository
    {
        Task<Role> CreateAsync(Role role);

        Task<Role> GetAsync(int roleId);

        //Task<Role> GetRoleInProjectAsync(int userId, int projectId);
        //Task<Role> GetRoleByUserIdAsync(int userId);

        Task DeleteAsync(int roleId);

        IEnumerable<Role> Get();
    }
}
