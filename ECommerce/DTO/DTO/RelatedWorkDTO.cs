using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO
{
    public class RelatedWorkDTO
    {
        public int ID { get; set; }
        [Required]
        public string Describtion { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Photo { get; set; }
        [Required]
        public int ProductId { get; set; }
   
    }
}
