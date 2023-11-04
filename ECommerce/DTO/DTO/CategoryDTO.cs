using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class CategoryDTO
    {
        public CategoryDTO()
        {
            Products = new HashSet<ProductDTO>();
        }
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Describtion { get; set; }
        
        public string Photo { get; set; }

        public IEnumerable<ProductDTO> Products { get; set; }
    }
}
