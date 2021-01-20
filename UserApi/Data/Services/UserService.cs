using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading.Tasks;
using UserApi.Data.Repositories.Contracts;
using UserApi.Data.Services.Contracts;
using UserApi.Models;

namespace UserApi.Data.Services
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CreateAsync(User user)
            => await _userRepository.CreateAsync(user);

        public async Task<User> FirstOrDefaultAsync(Expression<Func<User, bool>> predicate = null)
            => await _userRepository.FirstOrDefaultAsync(predicate);

        public async Task<IEnumerable<User>> GetUsersByUsernameAsync(string userName)
            => await _userRepository.GetAsync(userName);

        public async Task<IEnumerable<User>> GetAsync()
            => await _userRepository.GetAsync();

        public async Task<User> UpdateAsync(User user)
            => await _userRepository.UpdateAsync(user);

        public async Task DeleteAsync(Guid userId)
            => await _userRepository.DeleteAsync(userId);

        public async Task DeletePhotoAsync(Guid userId)
            => await _userRepository.DeletePhotoAsync(userId);
    }
}
