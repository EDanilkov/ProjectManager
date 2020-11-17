using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserApi.Data.Repositories;
using UserApi.Data.Repositories.Contracts;
using UserApi.Models;

namespace UserApi.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserRepository userRepository;

        public UserController(ProjectManagerContext db)
        {
            userRepository = new UserRepository(db);
        }

        [HttpPost()]
        public async Task<User> CreateAsync([FromBody] User user)
            => await userRepository.CreateAsync(user);

        [HttpGet("{userId}")]
        public async Task<User> GetAsync(int userId)
            => await userRepository.GetAsync(userId);

        [HttpGet("search/{userName}")]
        public IEnumerable<User> GetByUserName(string userName)
            => userRepository.Get(userName);

        [HttpGet()]
        public IEnumerable<User> Get()
            => userRepository.Get();

        [HttpDelete("{userId}")]
        public async Task DeleteAsync(int userId)
            => await userRepository.DeleteAsync(userId);

        [HttpPut]
        public async Task<User> UpdateAsync([FromBody] User user)
            => await userRepository.UpdateAsync(user);
    }
}
