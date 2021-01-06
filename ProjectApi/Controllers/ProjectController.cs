using Microsoft.AspNetCore.Mvc;
using ProjectApi.Data.Repositories;
using ProjectApi.Data.Repositories.Contracts;
using ProjectApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        IProjectRepository projectRepository;

        public ProjectController(ProjectManagerContext db)
        {
            projectRepository = new ProjectRepository(db);
        }

        [HttpPost()]
        public async Task<Project> CreateAsync([FromBody] Project project)
            => await projectRepository.CreateAsync(project);

        [HttpGet("{projectId}")]
        public async Task<Project> GetAsync(int projectId)
            => await projectRepository.GetAsync(projectId);

        [HttpGet("{projectId}/users")]
        public IEnumerable<int> GetUsersIdFromProject(int projectId)
            => projectRepository.GetUsersIdFromProject(projectId);

        [HttpGet("search/{projectName}")]
        public IEnumerable<Project> GetByProjectName(string projectName)
            => projectRepository.Get(projectName);

        [HttpGet("user/{userId}")]
        public async Task<IEnumerable<Project>> GetProjectsByUserIdAsync(int userId)
            => await projectRepository.GetProjectsByUserIdAsync(userId);

        [HttpGet()]
        public IEnumerable<Project> Get()
            => projectRepository.Get();

        [HttpDelete("{projectId}")]
        public async Task DeleteAsync(int projectId)
            => await projectRepository.DeleteAsync(projectId);

        [HttpPut]
        public async Task<Project> UpdateAsync([FromBody] Project project)
            => await projectRepository.UpdateAsync(project);

    }
}
