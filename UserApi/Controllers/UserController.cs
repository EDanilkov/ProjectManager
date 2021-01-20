using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using UserApi.Data.Repositories;
using UserApi.Data.Repositories.Contracts;
using UserApi.Data.Services.Contracts;
using UserApi.Models;

namespace UserApi.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserService _userService;
        IImageManagerService _imageManagerService;

        public UserController(IUserService userService, IImageManagerService imageManagerService)
        {
            _userService = userService;
            _imageManagerService = imageManagerService;
        }

        [HttpPost()]
        public async Task<User> CreateAsync([FromBody] User user)
            => await _userService.CreateAsync(user);

        [HttpGet("{userId}")]
        public async Task<User> GetAsync(Guid userId)
            => await _userService.FirstOrDefaultAsync(u => string.Equals(u.Id, userId));

        [HttpGet("name/{username}")]
        public async Task<User> GetUserByUsernameAsync(string username)
            => await _userService.FirstOrDefaultAsync(u => string.Equals(u.Username, username));

        [HttpGet("search/{userName}")]
        public async Task<IEnumerable<User>> GetUsersByUsernameAsync(string userName)
            => await _userService.GetUsersByUsernameAsync(userName);

        [HttpGet()]
        public async Task<IEnumerable<User>> GetAsync()
            => await _userService.GetAsync();

        [HttpDelete("{userId}")]
        public async Task DeleteAsync(Guid userId)
            => await _userService.DeleteAsync(userId);

        [HttpPut]
        public async Task<User> UpdateAsync([FromBody] User user)
            => await _userService.UpdateAsync(user);

        [Route("{userId}/photo")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<string> UploadPhotoAsync(Guid userId)
        {
            string base64ImageRepresentation = "";
            var uploadedFile = Request.Form.Files[0];
            if (uploadedFile != null)
            {
                base64ImageRepresentation = _imageManagerService.ConvertFileToBase64(uploadedFile);
                await _imageManagerService.AddPhotoAsync(base64ImageRepresentation, userId);
            }
            return JsonSerializer.Serialize(base64ImageRepresentation);
        }

        [Route("{userId}/photo")]
        [HttpDelete]
        public async Task DeletePhotoAsync(Guid userId)
        {
            await _userService.DeletePhotoAsync(userId);
        }
    }
}
