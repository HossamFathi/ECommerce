using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBaseLayer.models
{
    public class Product
    {
        
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } 
    
        public string ArabicName { get; set; }
        [Required]
        public string Describtion { get; set; }
       
        public string ArabicDescribtion { get; set; }

        
        public string Video { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryID { get; set; }
        public Category Category { get; set; }

        public ICollection<Photo> Photos { get; set; }
        public ICollection<RelatedWork> relatedWorks { get; set; }

    }
}
