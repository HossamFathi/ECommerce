using DTO.Entities.Photo;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO.Entities.RelatedWork
{
    public class AddRelatedWorkDTO : BaseRelatedWorkDTO , IFileImage
    {

        [Required]
        public string Describtion { get; set; }
        [Required]
        public string ArabicDescribtion { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ArabicName { get; set; }
        public IFormFile? ImageOrFile { get; set; }

        internal string Photo;
        public void SetPhotoUrl(string photoUrl) { Photo = photoUrl; }
        public string GetPhotoUrl() {  return Photo; }

    }
}
