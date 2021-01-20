using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UserApi.Models;

namespace UserApi.Data.Repositories.Contracts
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);

        Task<User> UpdateAsync(User user);

        Task<IEnumerable<User>> GetAsync();

        Task<IEnumerable<User>> GetAsync(string userName);

        Task<User> FirstOrDefaultAsync(Expression<Func<User, bool>> predicate = null);

        Task DeleteAsync(Guid userId);

        Task AddPhotoAsync(string base64ImageRepresentation, Guid userId);

        Task DeletePhotoAsync(Guid userId);
    }
}
