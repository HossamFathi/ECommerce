using System.ComponentModel.DataAnnotations.Schema;

namespace DataBaseLayer.models
{
    public class RelatedWork
    {
        public int ID { get; set; }
        public string Describtion { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }

        [ForeignKey(nameof(product))]
        public int ProductId { get; set; }
        public Product product { get; set; }
    }
}
