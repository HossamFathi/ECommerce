using System.ComponentModel.DataAnnotations;


namespace DTO
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

        IEnumerable<PhotoDTO> Photos { get; set; }
        public ProductDTO()
        {
            Photos = new HashSet<PhotoDTO>();
        }

       public void InsertPhotos(IEnumerable<PhotoDTO> photos)
        {
            Photos = photos;
        }




    }
}
