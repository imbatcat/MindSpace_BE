using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Services.ImageServices;

namespace FUNewsManagement.Services
{
    internal class ImageService : IImageService
    {
        private readonly Cloudinary _cloudinary;
        private readonly ILogger<ImageService> _logger;

        public ImageService(IConfiguration configuration, ILogger<ImageService> logger)
        {
            _logger = logger;
            _cloudinary = new Cloudinary(configuration["Cloudinary:ApiUrl"]);
            _cloudinary.Api.Secure = true;
        }

        public async Task<string> UploadImage(ImageUploadParams imageUploadParams)
        {
            try
            {
                var uploadResult = await _cloudinary.UploadAsync(imageUploadParams);
                return uploadResult.SecureUrl.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error uploading file {ex}", ex);
                return string.Empty;
            }
        }
    }
}