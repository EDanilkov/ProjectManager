using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Models;

namespace UserApi.Data.Repositories.Contracts
{
    interface IUserRepository
    {
        Task<User> CreateAsync(User user);

        Task DeleteAsync(int userId);

        IEnumerable<User> Get();

        Task<User> GetAsync(int userId);

        Task<User> GetAsync(string username);

        IEnumerable<User> Get(string userName);

        Task<User> UpdateAsync(User user);

        Task AddPhoto(string base64ImageRepresentation, int userId);

        Task DeletePhoto(int userId);
    }
}
