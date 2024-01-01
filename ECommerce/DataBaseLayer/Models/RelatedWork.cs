using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBaseLayer.models
{
    public class RelatedWork
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Describtion { get; set; }
        [Required]
        public string ArabicDescribtion { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ArabicName { get; set; }
        public string Photo { get; set; }
        [ForeignKey(nameof(product))]
        public int ProductId { get; set; }
        public Product product { get; set; }
    }
}
