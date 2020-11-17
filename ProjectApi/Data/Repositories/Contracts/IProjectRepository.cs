using ProjectApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectApi.Data.Repositories.Contracts
{
    interface IProjectRepository
    {
        Task<Project> CreateAsync(Project project);

        Task DeleteAsync(int projectId);

        IEnumerable<Project> Get();

        Task<Project> GetAsync(int projectId);

        IEnumerable<Project> Get(string projectName);

        Task<Project> UpdateAsync(Project project);
    }
}
