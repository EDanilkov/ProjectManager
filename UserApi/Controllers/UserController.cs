using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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

        [HttpGet("name/{username}")]
        public async Task<User> GetUserByUsername(string username)
            => await userRepository.GetAsync(username);

        [HttpGet("search/{userName}")]
        public IEnumerable<User> GetUsersByUsername(string userName)
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

        [Route("{userId}/photo")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<string> UploadPhoto(int userId)
        {
            string base64ImageRepresentation = "";
            var uploadedFile = Request.Form.Files[0];
            if (uploadedFile != null)
            {
                using var fileStream = uploadedFile.OpenReadStream();
                byte[] bytes = new byte[uploadedFile.Length];
                fileStream.Read(bytes, 0, (int)uploadedFile.Length);
                base64ImageRepresentation = "data:" + uploadedFile.ContentType + ";base64," + Convert.ToBase64String(bytes);
                await userRepository.AddPhoto(base64ImageRepresentation, userId);
            }
            return JsonSerializer.Serialize(base64ImageRepresentation);
        }

        [Route("{userId}/photo")]
        [HttpDelete]
        public async Task DeletePhoto(int userId)
        {
            await userRepository.DeletePhoto(userId);
        }
    }
}
