using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace DTO
{
    public class PhotoDTO : IFileImage
    {
        [Key]
        public int ID { get; set; }
        public int path { get; set; }
        public int ProductID { get; set; }
        public IFormFile ImageOrFile { get ; set ; }
    }
}
