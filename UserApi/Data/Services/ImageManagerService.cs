using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using UserApi.Data.Repositories;
using UserApi.Data.Repositories.Contracts;
using UserApi.Data.Services.Contracts;

namespace UserApi.Data.Services
{
    public class ImageManagerService : IImageManagerService
    {
        private readonly IUserRepository _userRepository;

        public ImageManagerService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string ConvertFileToBase64(IFormFile uploadedFile)
        {
            using var fileStream = uploadedFile.OpenReadStream();
            byte[] bytes = new byte[uploadedFile.Length];
            fileStream.Read(bytes, 0, (int)uploadedFile.Length);
            string base64ImageRepresentation = "data:" + uploadedFile.ContentType + ";base64," + Convert.ToBase64String(bytes);
            return base64ImageRepresentation;
        }

        public async Task AddPhotoAsync(string base64ImageRepresentation, Guid userId)
        {
            await _userRepository.AddPhotoAsync(base64ImageRepresentation, userId);
        }
    }
}
