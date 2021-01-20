using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Data.Repositories.Contracts;
using UserApi.Data.Services.Contracts;
using UserApi.Models;

namespace UserApi.Data.Services
{
    public class RoleService : IRoleService
    {
        IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Role> CreateAsync(Role role)
            => await _roleRepository.CreateAsync(role);

        public async Task<Role> GetAsync(Guid roleId)
            => await _roleRepository.GetAsync(roleId);

        public async Task<IEnumerable<Role>> GetAsync()
            => await _roleRepository.GetAsync();

        public async Task DeleteAsync(Guid roleId)
            => await _roleRepository.DeleteAsync(roleId);
    }
}
