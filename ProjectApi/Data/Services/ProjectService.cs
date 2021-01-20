using ProjectApi.Data.Repositories.Contracts;
using ProjectApi.Data.Services.Contracts;
using ProjectApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectApi.Data.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Project> CreateAsync(Project project)
            => await _projectRepository.CreateAsync(project);

        public async Task<IEnumerable<Project>> GetAsync()
            => await _projectRepository.GetAsync();

        public async Task<Project> GetAsync(Guid projectId)
            => await _projectRepository.GetAsync(projectId);

        public async Task<IEnumerable<Guid>> GetUserIdsByProjectAsync(Guid projectId)
            => await _projectRepository.GetUserIdsFromProject(projectId);

        public async Task<IEnumerable<Project>> BrowseProjectsAsync(string projectName, Guid userId)
            => await _projectRepository.GetAsync(projectName);

        public async Task<IEnumerable<Project>> GetProjectsByUserIdAsync(Guid userId)
            => await _projectRepository.GetProjectsByUserIdAsync(userId);

        public async Task DeleteAsync(Guid projectId)
            => await _projectRepository.DeleteAsync(projectId);

        public async Task<Project> UpdateAsync(Project project)
            => await _projectRepository.UpdateAsync(project);
    }
}
