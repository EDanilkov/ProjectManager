using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserApi.Data.Repositories;
using UserApi.Data.Repositories.Contracts;
using UserApi.Models;

namespace UserApi.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        IRoleRepository roleRepository;

        public RoleController(ProjectManagerContext db)
        {
            roleRepository = new RoleRepository(db);
        }

        [HttpPost]
        public async Task<Role> CreateAsync([FromBody] Role role)
            => await roleRepository.CreateAsync(role);

        [HttpGet("{roleId}")]
        public async Task<Role> GetAsync(int roleId)
            => await roleRepository.GetAsync(roleId);

        [HttpGet()]
        public IEnumerable<Role> Get()
            => roleRepository.Get();

        [HttpDelete("{roleId}")]
        public async Task DeleteAsync(int roleId)
            => await roleRepository.DeleteAsync(roleId);

        /*[HttpGet("{userId}/{projectId}")]
        public async Task<Role> GetRoleInProjectAsync(int userId, int projectId)
            => await roleRepository.GetRoleInProjectAsync(userId, projectId);*/




        /*[HttpGet("user/{userId}")]
        public async Task<Role> GetRoleByUserIdAsync(int userId)
            => await roleRepository.GetRoleByUserIdAsync(userId);*/
    }
}
