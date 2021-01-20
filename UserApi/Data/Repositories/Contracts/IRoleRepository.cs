using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserApi.Models;

namespace UserApi.Data.Repositories.Contracts
{
    public interface IRoleRepository
    {
        Task<Role> CreateAsync(Role role);

        Task<Role> GetAsync(Guid roleId);

        Task DeleteAsync(Guid roleId);

        Task<IEnumerable<Role>> GetAsync();
    }
}
