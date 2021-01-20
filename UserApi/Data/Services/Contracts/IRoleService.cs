using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Models;

namespace UserApi.Data.Services.Contracts
{
    public interface IRoleService
    {
        Task<Role> CreateAsync(Role role);

        Task<Role> GetAsync(Guid roleId);

        Task<IEnumerable<Role>> GetAsync();

        Task DeleteAsync(Guid roleId);
    }
}
