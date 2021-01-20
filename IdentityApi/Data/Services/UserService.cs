using IdentityApi.Data.Models;
using IdentityApi.Data.Repositories.Contracts;
using IdentityApi.Data.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApi.Data.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAsync()
            => await _userRepository.GetAsync();

        public async Task<User> CreateAsync(User user)
            => await _userRepository.CreateAsync(user);
    }
}
