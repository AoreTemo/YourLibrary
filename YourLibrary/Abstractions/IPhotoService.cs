using CloudinaryDotNet.Actions;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;

namespace YourLibrary.Services;

public interface IPhotoService
{
    Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
    Task<DeletionResult> DeletePhotoAsync(string publicId);
}