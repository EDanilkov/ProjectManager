using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace UserApi.Data.Services.Contracts
{
    public interface IImageManagerService
    {
        string ConvertFileToBase64(IFormFile uploadedFile);

        Task AddPhotoAsync(string base64ImageRepresentation, Guid userId);
    }
}
