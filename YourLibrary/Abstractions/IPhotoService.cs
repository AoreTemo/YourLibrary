using CloudinaryDotNet.Actions;

namespace YourLibrary.Abstractions;

public interface IPhotoService
{
    Task<ImageUploadResult> AddPhotoAsync(IFormFile? file);
    Task<DeletionResult> DeletePhotoAsync(string publicId);
}