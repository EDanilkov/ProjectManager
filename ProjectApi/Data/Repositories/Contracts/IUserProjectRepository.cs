using ProjectApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectApi.Data.Repositories.Contracts
{
    interface IUserProjectRepository
    {
        Task<UserProject> CreateAsync(UserProject userProject);

        Task<UserProject> GetAsync(int projectId, int userId);

        Task<UserProject> UpdateAsync(UserProject userProject);

        Task DeleteAsync(int projectId, int userId);

        Task DeleteAsync(int userProjectId);
    }
}
