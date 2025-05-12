using DTO.Entities.Photo;
using System.ComponentModel.DataAnnotations;


namespace DTO.Entities.Product
{
    public class ProductDTO
    {
        
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Describtion { get; set; }
        
        public string Video { get; set; }
       
        public int CategoryID { get; set; }

        public string? ImageURL { get; set; }

        public List<PhotoDTO> Photos { get; set; }
        public ProductDTO()
        {
            Photos = new List<PhotoDTO>();
        }

       public void InsertPhotos(List<PhotoDTO> photos)
        {
            Photos.AddRange(photos);
        }




    }
}
