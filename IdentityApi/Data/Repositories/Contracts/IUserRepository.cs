using IdentityApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IdentityApi.Data.Repositories.Contracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAsync();

        Task<User> FirstOrDefault(Expression<Func<User, bool>> predicate = null);

        Task<User> CreateAsync(User user);

        Task<User> UpdateAsync(User user);
    }
}
