using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace DTO.Entities.Photo
{
    public class AddPhotoDTO : IFileImage
    {
        
        public int ID { get; set; }
        string Path;
        public int ProductID { get; set; }
        public IFormFile ImageOrFile { get ; set ; }


        public void SetPath(string path) 
        {
            Path = path;
        }
        public string GetPath()
        {
            return Path;
        }
    }
}
