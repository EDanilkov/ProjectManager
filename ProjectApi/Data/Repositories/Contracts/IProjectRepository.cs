using ProjectApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectApi.Data.Repositories.Contracts
{
    public interface IProjectRepository
    {
        Task<Project> CreateAsync(Project project);

        Task DeleteAsync(Guid projectId);

        Task<IEnumerable<Project>> GetAsync();

        Task<Project> GetAsync(Guid projectId);

        Task<IEnumerable<Project>> GetAsync(string projectName);

        Task<IEnumerable<Project>> GetProjectsByUserIdAsync(Guid userId);

        Task<IEnumerable<Guid>> GetUserIdsFromProject(Guid projectId);

        Task<Project> UpdateAsync(Project project);
    }
}
