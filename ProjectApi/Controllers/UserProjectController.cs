using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectApi.Data.Repositories;
using ProjectApi.Data.Repositories.Contracts;
using ProjectApi.Models;

namespace ProjectApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserProjectController : ControllerBase
    {
        IUserProjectRepository userProjectRepository;

        public UserProjectController(ProjectManagerContext db)
        {
            userProjectRepository = new UserProjectRepository(db);
        }

        [HttpPost()]
        public async Task<UserProject> CreateAsync([FromBody] UserProject userProject)
            => await userProjectRepository.CreateAsync(userProject);

        [HttpGet("{projectId}/{userId}")]
        public async Task<UserProject> GetAsync(int projectId, int userId)
            => await userProjectRepository.GetAsync(projectId, userId);

        [HttpPut]
        public async Task<UserProject> UpdateUserProjectAsync([FromBody] UserProject userProject)
            => await userProjectRepository.UpdateAsync(userProject);

        [HttpDelete("{projectId}/{userId}")]
        public async Task DeleteAsync(int projectId, int userId)
            => await userProjectRepository.DeleteAsync(projectId, userId);

        [HttpDelete("{userProjectId}")]
        public async Task DeleteAsync(int userProjectId)
            => await userProjectRepository.DeleteAsync(userProjectId);
    }
}
