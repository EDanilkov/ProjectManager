using IdentityApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApi.Data.Services.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAsync();

        Task<User> CreateAsync(User user);
    }
}
