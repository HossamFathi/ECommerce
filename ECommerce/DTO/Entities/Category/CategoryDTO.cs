using System.ComponentModel.DataAnnotations;

namespace DTO.Entities.Category
{
    public class CategoryDTO
    {


        public int ID { get; set; }

        public string? Name { get; set; }

        public string? Describtion { get; set; }

        public string? Photo { get; set; }


    }
}
