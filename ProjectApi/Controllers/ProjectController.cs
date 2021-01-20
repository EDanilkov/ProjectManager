using Microsoft.AspNetCore.Mvc;
using ProjectApi.Data.Repositories;
using ProjectApi.Data.Repositories.Contracts;
using ProjectApi.Data.Services.Contracts;
using ProjectApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        IProjectService _projectService;
        IUserProjectService _userProjectService;

        public ProjectController(IProjectService projectService, IUserProjectService userProjectService)
        {
            _projectService = projectService;
            _userProjectService = userProjectService;
        }

        /*[HttpPost()]
        public async Task<Project> CreateAsync([FromBody] Project project)
            => await _projectService.CreateAsync(project);

        [HttpGet()]
        public async Task<IEnumerable<Project>> GetAsync()
            => await _projectService.GetAsync();

        [HttpGet("{projectId}")]
        public async Task<Project> GetAsync(Guid projectId)
            => await _projectService.GetAsync(projectId);

        [HttpGet("{projectId}/users")]
        public async Task<IEnumerable<Guid>> GetUserIdsByProjectAsync(Guid projectId)
            => await _projectService.GetUserIdsByProjectAsync(projectId);

        [HttpGet("search")]
        public async Task<IEnumerable<Project>> BrowseProjectsAsync([FromQuery] string projectName, [FromQuery] Guid userId)
            => await _projectService.BrowseProjectsAsync(projectName, userId);

        [HttpDelete("{projectId}")]
        public async Task DeleteAsync(Guid projectId)
            => await _projectService.DeleteAsync(projectId);

        [HttpPut()]
        public async Task<Project> UpdateAsync([FromBody] Project project)
            => await _projectService.UpdateAsync(project);*/






        [HttpPost()]
        public async Task<Project> CreateAsync([FromBody] Project project, [FromQuery] Guid roleId)
        {
            var createdProject = await _projectService.CreateAsync(project);

            var userProject = new UserProject()
            {
                ProjectId = createdProject.Id,
                RoleId = roleId,
                UserId = createdProject.AdminId
            };

            await _userProjectService.CreateAsync(userProject);
            return createdProject;
        }

        [HttpGet()]
        public async Task<IEnumerable<Project>> GetAsync()
            => await _projectService.GetAsync();

        [HttpGet("{projectId}")]
        public async Task<Project> GetAsync(Guid projectId)
            => await _projectService.GetAsync(projectId);

        [HttpGet("{projectId}/users")]
        public async Task<IEnumerable<Guid>> GetUserIdsByProjectAsync(Guid projectId)
            => await _projectService.GetUserIdsByProjectAsync(projectId);

        [HttpGet("search")]
        public async Task<IEnumerable<Project>> BrowseProjectsAsync([FromQuery] string projectName, [FromQuery] Guid userId)
            => await _projectService.BrowseProjectsAsync(projectName, userId);

        //TODO Delete and Move to browse 
        [HttpGet("user/{userId}")]
        public async Task<IEnumerable<Project>> GetProjectsByUserIdAsync(Guid userId)
            => await _projectService.GetProjectsByUserIdAsync(userId);

        [HttpDelete("{projectId}")]
        public async Task DeleteAsync(Guid projectId)
            => await _projectService.DeleteAsync(projectId);

        [HttpPut()]
        public async Task<Project> UpdateAsync([FromBody] Project project)
            => await _projectService.UpdateAsync(project);

        [HttpPost("{projectId}/user")]
        public async Task<UserProject> AddUserToProjectAsync(Guid projectId, [FromQuery] Guid userId, [FromQuery] Guid roleId)
            => await _userProjectService.CreateAsync(new UserProject()
            {
                ProjectId = projectId,
                UserId = userId,
                RoleId = roleId
            });

        [HttpGet("{projectId}/user/{userId}")]
        public async Task<UserProject> GetAsync(Guid projectId, Guid userId)
            => await _userProjectService.GetAsync(projectId, userId);

        [HttpPut("{projectId}/user/{userId}")]
        public async Task<UserProject> UpdateUserProjectAsync(Guid projectId, Guid userId, [FromQuery] Guid roleId)
        {
            var userproject = await GetAsync(projectId, userId);
            userproject.RoleId = roleId;
            return await _userProjectService.UpdateAsync(await GetAsync(projectId, userId));
        }

        [HttpDelete("{projectId}/user/{userId}")]
        public async Task DeleteAsync(Guid projectId, Guid userId)
            => await _userProjectService.DeleteAsync(projectId, userId);
    }
}
