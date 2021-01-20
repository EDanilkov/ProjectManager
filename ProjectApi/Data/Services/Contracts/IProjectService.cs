using ProjectApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectApi.Data.Services.Contracts
{
    public interface IProjectService
    {
        Task<Project> CreateAsync(Project project);

        Task<IEnumerable<Project>> GetAsync();

        Task<Project> GetAsync(Guid projectId);

        Task<IEnumerable<Guid>> GetUserIdsByProjectAsync(Guid projectId);

        Task<IEnumerable<Project>> BrowseProjectsAsync(string projectName, Guid userId);

        Task<IEnumerable<Project>> GetProjectsByUserIdAsync(Guid userId);

        Task DeleteAsync(Guid projectId);

        Task<Project> UpdateAsync(Project project);
    }
}
