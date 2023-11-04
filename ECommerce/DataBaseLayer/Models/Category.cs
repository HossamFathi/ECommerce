using System.ComponentModel.DataAnnotations;

namespace DataBaseLayer.models
{
    public class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Describtion { get; set; }
        
        public string Photo { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
