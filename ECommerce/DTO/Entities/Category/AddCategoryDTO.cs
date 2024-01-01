using DTO.Entities.Photo;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DTO.Entities.Category
{
    public class AddCategoryDTO : IFileImage
    {

        [Key]
        public int ID { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? ArabicName { get; set; }
        [Required]
        public string? Describtion { get; set; }
        [Required]
        public string? ArabicDescribtion { get; set; }
        string? PhotoPath;
        public IFormFile ImageOrFile { get; set; }

        public void SetPhotoPath(string photo)
        {
            PhotoPath = photo;
        }
        public string GetPhoto()
        {
            return PhotoPath ?? "";
        }

    }
}
