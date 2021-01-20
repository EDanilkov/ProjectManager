using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserApi.Data.Repositories;
using UserApi.Data.Repositories.Contracts;
using UserApi.Data.Services.Contracts;
using UserApi.Models;

namespace UserApi.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        public async Task<Role> CreateAsync([FromBody] Role role)
            => await _roleService.CreateAsync(role);

        [HttpGet("{roleId}")]
        public async Task<Role> GetAsync(Guid roleId)
            => await _roleService.GetAsync(roleId);

        [HttpGet()]
        public async Task<IEnumerable<Role>> GetAsync()
            => await _roleService.GetAsync();

        [HttpDelete("{roleId}")]
        public async Task DeleteAsync(Guid roleId)
            => await _roleService.DeleteAsync(roleId);
    }
}
