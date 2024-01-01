using DTO.Entities.Photo;
using System.ComponentModel.DataAnnotations;


namespace DTO.Entities.Product
{
    public class AddProductDTO
    {
        
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string ArabicName { get; set; }
        [Required]
        public string Describtion { get; set; }
        [Required]
        public string ArabicDescribtion { get; set; }
        public string Video { get; set; }
        [Required]
        public int CategoryID { get; set; }
    

    }
}
