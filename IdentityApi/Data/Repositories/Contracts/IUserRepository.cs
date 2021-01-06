using IdentityApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApi.Data.Repositories.Contracts
{
    interface IUserRepository
    {
        IEnumerable<User> Get();

        Task<User> CreateAsync(User user);

        Task<User> UpdateAsync(User user);
    }
}
