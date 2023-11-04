using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO
{
    public class PhotoDTO
    {
        [Key]
        public int ID { get; set; }
        public int path { get; set; }
        [ForeignKey(nameof(Product))]
        public int ProductID { get; set; }
        public ProductDTO Product { get; set; }
    }
}
