using DTO;
using Microsoft.AspNetCore.Http;

namespace ServiceLayer.Photos.Helper
{
    public interface IFileImageUploading
    {
        bool UploadPhoto(IFileImage FileOrImage, out string FileOrImagePath);
        bool UploadPhoto(IFormFile FileOrImage, out string FileOrImagePath);
        bool UploadFile(IFileImage FileOrImage, out string FileOrImagePath);
        bool UploadFile(IFileImage File, out string FileName, out byte[] FileData);
    }
}