using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO.Entities.RelatedWork
{
    public class BaseRelatedWorkDTO
    {
        public int ID { get; set; }
        [Required]
        public int ProductId { get; set; }
       

    }
}
