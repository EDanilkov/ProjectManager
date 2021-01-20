using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UserApi.Models;

namespace UserApi.Data.Services.Contracts
{
    public interface IUserService
    {
        Task<User> CreateAsync(User user);

        Task<User> FirstOrDefaultAsync(Expression<Func<User, bool>> predicate = null);

        Task<IEnumerable<User>> GetUsersByUsernameAsync(string userName);

        Task<IEnumerable<User>> GetAsync();

        Task DeleteAsync(Guid userId);

        Task<User> UpdateAsync(User user);

        Task DeletePhotoAsync(Guid userId);
    }
}
