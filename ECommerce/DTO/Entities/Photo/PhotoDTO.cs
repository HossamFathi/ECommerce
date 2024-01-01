using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace DTO.Entities.Photo
{
    public class PhotoDTO 
    {
        [Key]
        public int ID { get; set; }
        public string path { get; set; }
        public int ProductID { get; set; }

    }
}
