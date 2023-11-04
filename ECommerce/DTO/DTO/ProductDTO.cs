using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
       

     

    }
}
