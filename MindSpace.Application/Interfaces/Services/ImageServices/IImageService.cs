using CloudinaryDotNet.Actions;

namespace MindSpace.Application.Interfaces.Services.ImageServices
{
    public interface IImageService
    {
        Task<string> UploadImage(ImageUploadParams imageUploadParams);
    }
}
