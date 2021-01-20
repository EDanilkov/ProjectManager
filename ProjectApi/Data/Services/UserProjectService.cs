using ProjectApi.Data.Repositories.Contracts;
using ProjectApi.Data.Services.Contracts;
using ProjectApi.Models;
using System;
using System.Threading.Tasks;

namespace ProjectApi.Data.Services
{
    public class UserProjectService : IUserProjectService
    {
        private readonly IUserProjectRepository _userProjectRepository;

        public UserProjectService(IUserProjectRepository userProjectRepository)
        {
            _userProjectRepository = userProjectRepository;
        }

        public async Task<UserProject> CreateAsync(UserProject userProject)
            => await _userProjectRepository.CreateAsync(userProject);

        public async Task<UserProject> GetAsync(Guid projectId, Guid userId)
            => await _userProjectRepository.GetAsync(projectId, userId);

        public async Task<UserProject> UpdateAsync(UserProject userProject)
            => await _userProjectRepository.UpdateAsync(userProject);

        public async Task DeleteAsync(Guid projectId, Guid userId)
            => await _userProjectRepository.DeleteAsync(projectId, userId);

        public async Task DeleteAsync(Guid userProjectId)
            => await _userProjectRepository.DeleteAsync(userProjectId);
    }
}
