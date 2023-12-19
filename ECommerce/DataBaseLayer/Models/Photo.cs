using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBaseLayer.models
{
    public class Photo
    {
        [Key]
        public int ID { get; set; }
        public string path { get; set; }
        [ForeignKey(nameof(Product))]
        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}
